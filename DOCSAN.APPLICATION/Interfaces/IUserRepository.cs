using DOCSAN.CORE.Entities;

namespace DOCSAN.APPLICATION.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByMailAndPassword(string mail, string password);

        //Task<BasePaginatedDto<User>> Filter(IEnumerable<Specification<User>> filters, int limit, int offset);
    }
}
