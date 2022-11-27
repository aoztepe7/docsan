using DOCSAN.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DOCSAN.API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
          options.SuppressModelStateInvalidFilter = true);

            return services;
        }
    }
}
