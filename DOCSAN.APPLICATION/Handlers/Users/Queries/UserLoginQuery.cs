using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Messages.Users.Response;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DOCSAN.APPLICATION.Handlers.Users.Queries
{
    public class UserLoginQuery : IRequest<BaseDataResponse<UserLoginResponse>>
    {
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginQueryHandler : IRequestHandler<UserLoginQuery, BaseDataResponse<UserLoginResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginQueryHandler(IUserRepository userRepository, IJwtService jwtService, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _contextAccessor = contextAccessor;
        }

        public async Task<BaseDataResponse<UserLoginResponse>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByMailAndPassword(request.Mail, request.Password);

            var token = _jwtService.GenerateToken(user);
            return new BaseDataResponse<UserLoginResponse>(new UserLoginResponse { Token = token, User = user.Adapt<UserDto>() }, 200);
        }
    }
}
