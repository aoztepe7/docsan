using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Utils;
using DOCSAN.CORE.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DOCSAN.APPLICATION.Handlers.Projects.Commands
{
    public class ProjectCreateCommand : IRequest<BaseIdResponse>
    {
        public string Name { get; set; } = null!;
        public IFormFile? Image { get; set; }
    }

    public class ProjectCreateCommandHandler : IRequestHandler<ProjectCreateCommand, BaseIdResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IAwsS3Service _awsS3Service;

        public ProjectCreateCommandHandler(IProjectRepository projectRepository, IAwsS3Service awsS3Service)
        {
            _projectRepository = projectRepository;
            _awsS3Service = awsS3Service;
        }

        public async Task<BaseIdResponse> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
        {
            var imageUrl = String.Empty;
            if(request.Image != null)
            {
                var result = await _awsS3Service.UploadFileAsync(request.Image, request.Name , "Main");
                if (result.Success)
                    imageUrl = result.Url;
            }
           

            var project = new Project
            {
                Name = request.Name,
                ImageUrl = imageUrl,
                CreatedBy = AuditHelper.GetRequestOwner()
            };
            var id = await _projectRepository.AddAsync(project);

            return new BaseIdResponse(id, 200);
        }
    }
}
