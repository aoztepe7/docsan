using DOCSAN.SHARED.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DOCSAN.SHARED
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            services.Configure<AwsConfig>(configuration.GetSection("AwsConfig"));
            return services;
        }
    }
}
