using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Request;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.APPLICATION.Specifications;
using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Utils;
using Mapster;
using MediatR;

namespace DOCSAN.APPLICATION.Handlers.Projects.Queries
{
    public class ProjectFilterQuery : BaseFilterRequest, IRequest<BasePaginationListResponse<ProjectDto>>
    {
        public string Name { get; set; }
    }

    public class ProjectFilterQueryHandler : IRequestHandler<ProjectFilterQuery, BasePaginationListResponse<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectFilterQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<BasePaginationListResponse<ProjectDto>> Handle(ProjectFilterQuery query, CancellationToken cancellationToken)
        {
            var filters = GetSpecs(query);

            var pagingParameters = PagingHelper.GetPagingParameters(query.PageSize, query.PageNumber);
            var response = await _projectRepository.Filter(filters, pagingParameters["Skip"], pagingParameters["Take"]);
            var dtoList = new List<ProjectDto>();
            response.Data.ToList().ForEach(x => dtoList.Add(x.Adapt<ProjectDto>()));

            return new BasePaginationListResponse<ProjectDto>(dtoList, response.TotalCount, query.PageNumber, query.PageSize, 200, "Success");
        }

        private List<Specification<Project>> GetSpecs(ProjectFilterQuery query)
        {
            var filters = new List<Specification<Project>>();
            if (!string.IsNullOrEmpty(query.Name)) filters.Add(new ProjectSpecifications.NameSpecifiation(query.Name));
            return filters;
        }
    }
}
