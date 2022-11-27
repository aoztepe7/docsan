using DOCSAN.APPLICATION.Dtos;
using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Utils;

namespace DOCSAN.APPLICATION.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByMailAndPassword(string mail, string password);

        Task<BasePaginationDto<User>> Filter(IEnumerable<Specification<User>> filters, int limit, int offset);
    }
}
