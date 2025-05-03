using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Web.Extension
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddControllers();
            Services.AddOpenApi();
            Services.AddSwaggerGen();
            return Services;
        }
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;

            });
            return Services;
        }
        public static IServiceCollection AddJWTService(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTOptions:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JWTOptions:Audience"],


                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecurityKey"]))
                };
            });
            return Services;
        }
    }
}
