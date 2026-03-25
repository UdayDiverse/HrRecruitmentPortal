using DataAccess.Interfaces.Masters;
using DataAccess.Repositories.Masters;
using DataAccessLayer.Interfaces.Masters;
using DataAccessLayer.Repositories.Masters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataAccessLayer.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<ILookupTypeReporsitory, LookupTypeReporsitory>();
            services.AddScoped<ILookupReporsitory, LookupRepository>();
            return services;
        }
    }
}
