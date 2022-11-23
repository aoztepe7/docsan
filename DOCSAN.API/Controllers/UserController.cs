using DOCSAN.APPLICATION.Attributes;
using DOCSAN.APPLICATION.Handlers.Users.Queries;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Messages.Users.Response;
using Microsoft.AspNetCore.Mvc;

namespace DOCSAN.API.Controllers
{
    public class UserController : ApiControllerBase
    {

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<BaseDataResponse<UserLoginResponse>>> Login(UserLoginQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
