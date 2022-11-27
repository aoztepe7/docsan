using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Request;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Specifications;
using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Enums;
using DOCSAN.SHARED.Utils;
using Mapster;
using MediatR;

namespace DOCSAN.APPLICATION.Handlers.Users.Queries
{
    public class UserFilterQuery : BaseFilterRequest, IRequest<BasePaginationListResponse<UserDto>>
    {
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public enmGender? Gender { get; set; }
        public enmRole? Role { get; set; }
    }

    public class UserFilterQueryHandler : IRequestHandler<UserFilterQuery, BasePaginationListResponse<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public UserFilterQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BasePaginationListResponse<UserDto>> Handle(UserFilterQuery query, CancellationToken cancellationToken)
        {
            var filters = GetSpecs(query);

            var pagingParameters = PagingHelper.GetPagingParameters(query.PageSize, query.PageNumber);
            var response = await _userRepository.Filter(filters, pagingParameters["Skip"], pagingParameters["Take"]);
            var dtoList = new List<UserDto>();
            response.Data.ToList().ForEach(x => dtoList.Add(x.Adapt<UserDto>()));

            return new BasePaginationListResponse<UserDto>(dtoList, response.TotalCount, query.PageNumber, query.PageSize, 200, "Success");
        }

        private List<Specification<User>> GetSpecs(UserFilterQuery query)
        {
            var filters = new List<Specification<User>>();
            if (!string.IsNullOrEmpty(query.Mail)) filters.Add(new UserSpecifications.MailSpecifiation(query.Mail));
            if (!string.IsNullOrEmpty(query.FirstName)) filters.Add(new UserSpecifications.FirstNameSpecifiation(query.FirstName));
            if (!string.IsNullOrEmpty(query.LastName)) filters.Add(new UserSpecifications.LastNameSpecifiation(query.LastName));
            if (query.Role != null) filters.Add(new UserSpecifications.RoleSpecifiation(query.Role.Value));
            if (query.Gender != null) filters.Add(new UserSpecifications.GenderSpecifiation(query.Gender.Value));
            return filters;
        }
    }
}
