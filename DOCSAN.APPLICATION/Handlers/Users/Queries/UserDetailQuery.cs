using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Exceptions;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Request;
using DOCSAN.APPLICATION.Messages.Common.Response;
using Mapster;
using MediatR;

namespace DOCSAN.APPLICATION.Handlers.Users.Queries
{
    public class UserDetailQuery : BaseIdRequest, IRequest<BaseDataResponse<UserDto>>
    {
    }

    public class UserDetailCommandHandler : IRequestHandler<UserDetailQuery, BaseDataResponse<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public UserDetailCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseDataResponse<UserDto>> Handle(UserDetailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
                throw new DomainNotFoundException("User");

            var userDto = user.Adapt<UserDto>();

            return new BaseDataResponse<UserDto>(userDto, 200);
        }
    }
}
