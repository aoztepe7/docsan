using DOCSAN.INFRASTRUCTURE.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.INFRASTRUCTURE.Services
{
    public static class MigrationService
    {
        public static IHost MigrateDatabase(this IHost host, IConfiguration configuration)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<DatabaseMigration>();
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    databaseService.CreateDatabase("docsan", configuration);
                    runner.ListMigrations();
                    runner.MigrateUp();
                }
                catch
                {
                    throw;
                }
            }
            return host;
        }
    }
}
