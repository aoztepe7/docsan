using DOCSAN.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        Guid ValidateToken(string token);
        string GetUserMail(string token);
    }
}
