using Microsoft.Extensions.DependencyInjection;
using ServiceImplementation.MappingProfiles;

namespace ServiceImplementation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(ProductProfile).Assembly);

            return services;
        }
    }
}
