
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using ServiceImplementation.IdentityService;
using ServiceImplementation.MappingProfiles;
using ServiceImplementation.ServiceManager;
using ServiceImplementation.Services;

namespace ServiceImplementation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddScoped<IServiceManager, ServiceManagerFactoryDelegate>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService>>(Providor=> 
            () => Providor.GetRequiredService<IProductService>());

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(Providor => 
            () => Providor.GetRequiredService<IBasketService>());

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(Providor =>
            () => Providor.GetRequiredService<IAuthenticationService>());
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<Func<IOrderService>>(Providor =>
            () => Providor.GetRequiredService<IOrderService>());

            return services;
        }
    }
}
