using DOCSAN.APPLICATION.Dtos;
using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Utils;

namespace DOCSAN.APPLICATION.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<BasePaginationDto<Project>> Filter(IEnumerable<Specification<Project>> filters, int limit, int offset);
    }
}
