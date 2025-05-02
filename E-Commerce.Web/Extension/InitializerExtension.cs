using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;

namespace E_Commerce.Web.Extension
{
    public static class InitializerExtension
    {
        public static async Task InitializeDataBase(this IApplicationBuilder builder)
        {
            using var Scope =builder.ApplicationServices.CreateScope();
            var Initializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
           await Initializer.SeedAsync();
           await Initializer.IntializeAsync();
            await Initializer.IdentitySeedAsync();
        } 

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustmExeptionHandlerMiddleWare>();
            return app;
        }
    }
}
