using DOCSAN.APPLICATION.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Messages.Users.Response
{
    public class UserLoginResponse
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
