using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using ServiceImplementation.MappingProfiles;

namespace ServiceImplementation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddScoped<IServiceManager, ServiceManager.ServiceManger>();

            return services;
        }
    }
}
