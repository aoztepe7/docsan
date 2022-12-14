using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DOCSAN.INFRASTRUCTURE.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfig _jwtConfig;

        public JwtService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id",user.Id.ToString()),
                    new Claim("mail",user.Mail.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SecurityToken? GetValidatedToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                return validatedToken;
            }
            catch
            {
                return null;
            }
        }

        public string GetUserMail(string token)
        {
            if (token == null)
                return String.Empty;

            var validatedToken = GetValidatedToken(token);
            if (validatedToken == null)
                return String.Empty;

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userMail = jwtToken.Claims.First(x => x.Type == "mail").Value;
            return userMail;
        }

        public Guid ValidateToken(string token)
        {
            if (token == null)
                return Guid.Empty;

            var validatedToken = GetValidatedToken(token);
            if (validatedToken == null)
                return Guid.Empty;

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            return userId;
        }
    }
}
