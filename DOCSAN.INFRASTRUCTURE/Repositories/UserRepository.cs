using Dapper;
using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Utils;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DOCSAN.INFRASTRUCTURE.Repositories
{
    public class UserRepository : DapperRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<Guid> AddAsync(User entity)
        {
            string tSql = @"insert into Users (Id,Created,CreatedBy,Status,Deleted,Mail,Password,Role,FirstName,LastName,BirthDate,Gender)
                           values (@Id,@Created,@CreatedBy,@Status,@Deleted,@Mail,@Password,@Role,@FirstName,@LastName,@BirthDate,@Gender)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Guid);
            parameters.Add("Created", entity.Created, DbType.DateTime);
            parameters.Add("CreatedBy", entity.CreatedBy, DbType.String);
            parameters.Add("Status", entity.Status, DbType.Boolean);
            parameters.Add("Deleted", entity.Deleted, DbType.Boolean);
            parameters.Add("Mail", entity.Mail, DbType.String);
            parameters.Add("Password", entity.Password, DbType.String);
            parameters.Add("Role", entity.Role, DbType.Byte);
            parameters.Add("FirstName", entity.FirstName, DbType.String);
            parameters.Add("LastName", entity.LastName, DbType.String);
            parameters.Add("BirthDate", entity.BirthDate, DbType.DateTime);
            parameters.Add("Gender", entity.Gender, DbType.Byte);
          
            await base.ExecuteAsync(tSql, parameters);
            return entity.Id;
        }

        public async Task<int> DeleteAsync(User entity)
        {
            string tSql = @"update Users set Deleted=1,LastModified=@LastModified,LastModifiedBy=@LastModifiedBy where Id=@Id";
            return await base.ExecuteAsync(tSql, new { Id = entity.Id, LastModified = entity.LastModified, LastModifiedBy = entity.LastModifiedBy });
        }

        public async Task<BasePaginationDto<User>> Filter(IEnumerable<Specification<User>> filters, int limit, int offset)
        {
            var tSql = @"select * from Users where deleted=0";
            var result = await base.QueryAsync<User>(tSql);
            var totalCount = result.Count();
            var users = result.AsQueryable();
            if (filters != null && users != null)
            {
                foreach (var filter in filters)
                {
                    users = users!.Where(filter.ToExpression());
                }
            }
            var filteredResult = users!.Skip(limit).Take(offset);
            return new BasePaginationDto<User>(filteredResult, totalCount);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            string tSql = @"select * from Users where Id=@Id and Deleted=0";
            return await base.QueryFirstOrDefaultAsync<User>(tSql, new { Id = id });
        }

        public async Task<User> GetByMailAndPassword(string mail, string password)
        {
            string tSql = @"select * from Users where Mail=@Mail and Password=@Password and Deleted=0";
            return await base.QueryFirstOrDefaultAsync<User>(tSql, new { Mail = mail , Password = password });
        }

        public async Task<int> UpdateAsync(User entity)
        {
            string tSql = @"update Users set Status=@Status,Mail=@Mail,Password=@Password,Role=@Role,LastModified=@LastModified,LastModifiedBy=@LastModifiedBy,
                            FirstName=@FirstName,LastName=@LastName,BirthDate=@BirthDate,Gender=@Gender where Id=@Id and Deleted=0";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Guid);          
            parameters.Add("Status", entity.Status, DbType.Boolean);
            parameters.Add("Deleted", entity.Deleted, DbType.Boolean);
            parameters.Add("Mail", entity.Mail, DbType.String);
            parameters.Add("Password", entity.Password, DbType.String);
            parameters.Add("Role", entity.Role, DbType.Byte);
            parameters.Add("LastModified", entity.Created, DbType.DateTime);
            parameters.Add("LastModifiedBy", entity.CreatedBy, DbType.String);
            parameters.Add("FirstName", entity.FirstName, DbType.String);
            parameters.Add("LastName", entity.LastName, DbType.String);
            parameters.Add("BirthDate", entity.BirthDate, DbType.DateTime);
            parameters.Add("Gender", entity.Gender, DbType.Byte);

            return await base.ExecuteAsync(tSql, parameters);
        }
    }
}
