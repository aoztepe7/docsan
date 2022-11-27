using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Exceptions;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Request;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Utils;
using DOCSAN.SHARED.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DOCSAN.APPLICATION.Handlers.Users.Commands
{
    public class UserUpdateCommand : BaseIdRequest, IRequest<BaseDataResponse<UserDto>>
    {
        public string Mail { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public enmGender Gender { get; set; }
        public enmRole Role { get; set; }
        public IFormFile? Image { get; set; }
    }

    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, BaseDataResponse<UserDto>>
    {
        private readonly IAwsS3Service _awsS3Service;
        private readonly IUserRepository _userRepository;

        public UserUpdateCommandHandler(IUserRepository userRepository, IAwsS3Service awsS3Service)
        {
            _userRepository = userRepository;
            _awsS3Service = awsS3Service;
        }

        public async Task<BaseDataResponse<UserDto>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var existUser = await _userRepository.GetByIdAsync(request.Id);
            if (existUser == null)
                throw new DomainNotFoundException("User");

            var imageUrl = String.Empty;
            if (request.Image != null)
            {
                var result = await _awsS3Service.UploadFileAsync(request.Image, "User", "Profile");
                if (result.Success)
                    imageUrl = result.Url;
            }

            existUser.Mail = request.Mail;
            existUser.BirthDate = request.BirthDate;
            existUser.FirstName = request.FirstName;
            existUser.LastName = request.LastName;
            existUser.Gender = request.Gender;
            existUser.LastModified = DateTime.Now;
            existUser.LastModifiedBy = AuditHelper.GetRequestOwner();
            existUser.Role = request.Role;
            existUser.ImageUrl = imageUrl;

            await _userRepository.UpdateAsync(existUser);

            return new BaseDataResponse<UserDto>(existUser.Adapt<UserDto>(), 200);
        }
    }
}
