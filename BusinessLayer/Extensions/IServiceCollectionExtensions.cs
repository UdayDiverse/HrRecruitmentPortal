using BusinessLayer.Interfaces.Masters;
using BusinessLayer.Mappings.Masters;
using BusinessLayer.Services.Masters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusinessLayer.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddAutoMapper(typeof(DepartmentMappingProfile).Assembly);

            return services;
        }
    }
}
