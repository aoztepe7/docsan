using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.INFRASTRUCTURE.Migrations;
using DOCSAN.INFRASTRUCTURE.Repositories;
using DOCSAN.INFRASTRUCTURE.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DOCSAN.INFRASTRUCTURE
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DatabaseMigration>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IAwsS3Service, AwsS3Service>();
            services.AddTransient<IJwtService, JwtService>();
            //services.AddTransient<IMailService, MailService>();
            return services;
        }
    }
}
