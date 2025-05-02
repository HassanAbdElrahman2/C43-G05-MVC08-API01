using ServiceImplementation;
using E_Commerce.Web.Extension;
using Persistence;
using E_Commerce.Web.CustomMiddleWares;
namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddSwaggerServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();

            #endregion

           var app = builder.Build();

            #region InitilizeDatabase

            await app.InitializeDataBase();

            #endregion

            #region Configure the HTTP request pipeline.
            app.UseCustomExceptionMiddleWare();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
