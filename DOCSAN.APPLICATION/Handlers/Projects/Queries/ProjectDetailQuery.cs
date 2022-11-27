using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Exceptions;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Request;
using DOCSAN.APPLICATION.Messages.Common.Response;
using Mapster;
using MediatR;

namespace DOCSAN.APPLICATION.Handlers.Projects.Queries
{
    public class ProjectDetailQuery : BaseIdRequest, IRequest<BaseDataResponse<ProjectDto>>
    {
    }
    public class ProjectDetailQueryHandler : IRequestHandler<ProjectDetailQuery, BaseDataResponse<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectDetailQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<BaseDataResponse<ProjectDto>> Handle(ProjectDetailQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project == null)
                throw new DomainNotFoundException("Project");

            var projectDto = project.Adapt<ProjectDto>();

            return new BaseDataResponse<ProjectDto>(projectDto, 200);
        }
    }
}
