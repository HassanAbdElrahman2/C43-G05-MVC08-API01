using DomainLayer.Contracts;

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
        } 
    }
}
