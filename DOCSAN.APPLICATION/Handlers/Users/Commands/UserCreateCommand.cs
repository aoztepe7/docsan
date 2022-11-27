using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Utils;
using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Enums;
using MediatR;

namespace DOCSAN.APPLICATION.Handlers.Users.Commands
{
    public class UserCreateCommand : IRequest<BaseIdResponse>
    {
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public enmRole Role { get; set; }
    }

    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, BaseIdResponse>
    {
        private readonly IUserRepository _userRepository;

        public UserCreateCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseIdResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Mail = request.Mail,
                Password = request.Password,
                Role = request.Role,
                CreatedBy = AuditHelper.GetRequestOwner()
            };

            var id = await _userRepository.AddAsync(user);


            return new BaseIdResponse(id, 200);
        }
    }
}
