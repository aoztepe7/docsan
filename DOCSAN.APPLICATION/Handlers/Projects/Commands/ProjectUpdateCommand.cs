using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Exceptions;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Request;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Utils;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DOCSAN.APPLICATION.Handlers.Projects.Commands
{
    public class ProjectUpdateCommand : BaseIdRequest, IRequest<BaseDataResponse<ProjectDto>>
    {
        public string Name { get; set; } = null!;
        public IFormFile? Image { get; set; }
    }

    public class ProjectUpdateCommandHandler : IRequestHandler<ProjectUpdateCommand, BaseDataResponse<ProjectDto>>
    {
        private readonly IAwsS3Service _awsS3Service;
        private readonly IProjectRepository _projectRepository;

        public ProjectUpdateCommandHandler(IProjectRepository projectRepository, IAwsS3Service awsS3Service)
        {
            _projectRepository = projectRepository;
            _awsS3Service = awsS3Service;
        }

        public async Task<BaseDataResponse<ProjectDto>> Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
        {
            var existProject = await _projectRepository.GetByIdAsync(request.Id);
            if (existProject == null)
                throw new DomainNotFoundException("Project");

            var imageUrl = String.Empty;
            if (request.Image != null)
            {
                var result = await _awsS3Service.UploadFileAsync(request.Image, request.Name, "Main");
                if (result.Success)
                    imageUrl = result.Url;
            }

            existProject.Name = request.Name;
            existProject.ImageUrl = imageUrl;
            existProject.LastModified = DateTime.Now;
            existProject.LastModifiedBy = AuditHelper.GetRequestOwner();

            await _projectRepository.UpdateAsync(existProject);
            return new BaseDataResponse<ProjectDto>(existProject.Adapt<ProjectDto>(), 200);

        }
    }
}
