using DOCSAN.APPLICATION.Exceptions;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Request;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Utils;
using MediatR;

namespace DOCSAN.APPLICATION.Handlers.Projects.Commands
{
    public class ProjectDeleteCommand : BaseIdRequest, IRequest<BaseResponse>
    {
    }
    public class ProjectDeleteCommandHandler : IRequestHandler<ProjectDeleteCommand, BaseResponse>
    {
        private readonly IProjectRepository _projetRepository;
        private readonly IAwsS3Service _awsS3Service;

        public ProjectDeleteCommandHandler(IProjectRepository projetRepository, IAwsS3Service awsS3Service)
        {
            _projetRepository = projetRepository;
            _awsS3Service = awsS3Service;
        }

        public async Task<BaseResponse> Handle(ProjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var project = await _projetRepository.GetByIdAsync(request.Id);
            if (project == null)
                throw new DomainNotFoundException("Project");



            await _awsS3Service.DeleteObjectFromS3Async(project.ImageUrl);
            project.LastModified = DateTime.Now;
            project.LastModifiedBy = AuditHelper.GetRequestOwner();

            await _projetRepository.DeleteAsync(project);

            return new BaseResponse(200);
        }
    }

}
