using DOCSAN.APPLICATION.Attributes;
using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Handlers.Projects.Commands;
using DOCSAN.APPLICATION.Handlers.Projects.Queries;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.SHARED.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DOCSAN.API.Controllers
{
    public class ProjectController : ApiControllerBase
    {
        [Authorize(enmRole.Admin)]
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<BaseIdResponse>> Create([FromForm] ProjectCreateCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(enmRole.Admin)]
        [Route("detail")]
        [HttpPost]
        public async Task<ActionResult<BaseDataResponse<ProjectDto>>> Detail(ProjectDetailQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize(enmRole.Admin)]
        [Route("update")]
        [HttpPost]
        public async Task<ActionResult<BaseDataResponse<ProjectDto>>> Update(ProjectUpdateCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(enmRole.Admin)]
        [Route("delete")]
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Delete(ProjectDeleteCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(enmRole.Admin)]
        [Route("list")]
        [HttpPost]
        public async Task<ActionResult<BasePaginationListResponse<ProjectDto>>> List(ProjectFilterQuery query)
        {
            return await Mediator.Send(query);
        }


    }
}
