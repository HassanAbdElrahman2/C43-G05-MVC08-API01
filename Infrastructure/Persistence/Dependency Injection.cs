using DomainLayer.Contracts;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Data.DbInitializer;
using Persistence.Data.Identity;
using Persistence.Repositories;
using Persistence.UnitOfWorks;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class Dependency_Injection
    {
        public static void AddPersistenceServices( this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDBContext>
                (optionsAction:option=>option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddDbContext<StoreIdentityDbContext>
             (optionsAction: option => option.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            services.AddScoped<ICacheRepository, CacheRepository>();
        }
    }
}
