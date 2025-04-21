using DomainLayer.Contracts;

namespace E_Commerce.Web.Extension
{
    public static class InitializerExtension
    {
        public static void InitializeDataBase(this IApplicationBuilder builder)
        {
            using var Scope =builder.ApplicationServices.CreateScope();
            var Initializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            Initializer.Seed();
            Initializer.Intialize();
        } 
    }
}
