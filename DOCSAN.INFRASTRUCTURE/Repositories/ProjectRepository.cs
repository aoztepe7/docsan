using Dapper;
using DOCSAN.APPLICATION.Dtos;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Utils;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DOCSAN.INFRASTRUCTURE.Repositories
{
    public class ProjectRepository : DapperRepository, IProjectRepository
    {
        public ProjectRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Guid> AddAsync(Project entity)
        {
            string tSql = @"insert into Projects (Id,Created,CreatedBy,Status,Deleted,Name,ImageUrl)
                           values (@Id,@Created,@CreatedBy,@Status,@Deleted,@Name,@ImageUrl);";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Guid);
            parameters.Add("Created", entity.Created, DbType.DateTime);
            parameters.Add("CreatedBy", entity.CreatedBy, DbType.String);
            parameters.Add("Status", entity.Status, DbType.Boolean);
            parameters.Add("Deleted", entity.Deleted, DbType.Boolean);
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("ImageUrl", entity.ImageUrl, DbType.String);

            await base.ExecuteAsync(tSql, parameters);
            return entity.Id;
        }

        public async Task<int> DeleteAsync(Project entity)
        {
            string tSql = @"update Projects set Deleted=1,LastModified=@LastModified,LastModifiedBy=@LastModifiedBy where Id=@Id";
            return await base.ExecuteAsync(tSql, new { Id = entity.Id, LastModified = entity.LastModified, LastModifiedBy = entity.LastModifiedBy });
        }

        public async Task<BasePaginationDto<Project>> Filter(IEnumerable<Specification<Project>> filters, int limit, int offset)
        {
            var tSql = @"select * from Projects where deleted=0";
            var result = await base.QueryAsync<Project>(tSql);
            var totalCount = result.Count();
            var projects = result.AsQueryable();
            if (filters != null && projects != null)
            {
                foreach (var filter in filters)
                {
                    projects = projects!.Where(filter.ToExpression());
                }
            }
            var filteredResult = projects!.Skip(limit).Take(offset);
            return new BasePaginationDto<Project>(filteredResult, totalCount);
        }

        public async Task<Project> GetByIdAsync(Guid id)
        {
            string tSql = @"select * from Projects where Id=@Id and Deleted=0";
            return await base.QueryFirstOrDefaultAsync<Project>(tSql, new { Id = id });
        }

        public async Task<int> UpdateAsync(Project entity)
        {
            string tSql = @"update Projects set Status=@Status,Name=@Name,ImageUrl=@ImageUrl,LastModified=@LastModified,LastModifiedBy=@LastModifiedBy
                           where Id=@Id and Deleted=0";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Guid);
            parameters.Add("Status", entity.Status, DbType.Boolean);
            parameters.Add("Deleted", entity.Deleted, DbType.Boolean);
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("ImageUrl", entity.ImageUrl, DbType.String);
            parameters.Add("LastModified", entity.Created, DbType.DateTime);
            parameters.Add("LastModifiedBy", entity.CreatedBy, DbType.String);

            return await base.ExecuteAsync(tSql, parameters);
        }
    }
}
