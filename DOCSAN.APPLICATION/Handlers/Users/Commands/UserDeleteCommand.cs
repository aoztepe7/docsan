using DOCSAN.APPLICATION.Exceptions;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Request;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Utils;
using DOCSAN.SHARED.Utils;
using MediatR;

namespace DOCSAN.APPLICATION.Handlers.Users.Commands
{
    public class UserDeleteCommand : BaseIdRequest, IRequest<BaseResponse>
    {
    }

    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepository;

        public UserDeleteCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                throw new DomainNotFoundException("User");

            user.LastModified = DateTime.Now;
            user.LastModifiedBy = AuditHelper.GetRequestOwner();

            await _userRepository.DeleteAsync(user);

            return new BaseResponse(200);
        }
    }
}
