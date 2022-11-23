using Dapper;
using Microsoft.Extensions.Configuration;

namespace DOCSAN.INFRASTRUCTURE.Migrations
{
    public class DatabaseMigration : DapperRepository
    {
        public DatabaseMigration(IConfiguration configuration) : base(configuration)
        {
        }

        public void CreateDatabase(string dbName, IConfiguration configuration)
        {
            var query = "SELECT * FROM master.sys.databases where name=@name";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);

            using (var connection = base.CreateMasterConnection(configuration))
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}
