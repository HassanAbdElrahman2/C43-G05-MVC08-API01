using ServiceImplementation;
using E_Commerce.Web.Extension;
using Persistence;
using E_Commerce.Web.CustomMiddleWares;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using E_Commerce.Web.Factories;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;

            });
            


            #endregion

            var app = builder.Build();

            #region InitilizeDatabase

            await app.InitializeDataBase();

            #endregion

            #region Configure the HTTP request pipeline.
            app.UseMiddleware<CustmExeptionHandlerMiddleWare>();
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
