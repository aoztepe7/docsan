using DOCSAN.APPLICATION.Attributes;
using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Handlers.Users.Commands;
using DOCSAN.APPLICATION.Handlers.Users.Queries;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Messages.Users.Response;
using DOCSAN.SHARED.Enums;
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

        [Authorize(enmRole.Admin)]
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<BaseIdResponse>> Create(UserCreateCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(enmRole.Admin)]
        [Route("update")]
        [HttpPost]
        public async Task<ActionResult<BaseDataResponse<UserDto>>> Update([FromForm] UserUpdateCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(enmRole.Admin)]
        [Route("delete")]
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Delete(UserDeleteCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(enmRole.Admin)]
        [Route("detail")]
        [HttpPost]
        public async Task<ActionResult<BaseDataResponse<UserDto>>> Detail(UserDetailQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize(enmRole.Admin)]
        [Route("list")]
        [HttpPost]
        public async Task<ActionResult<BasePaginationListResponse<UserDto>>> List(UserFilterQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
