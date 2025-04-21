using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
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
            services.AddDbContext<StoreDBContext>(optionsAction:option=>option.UseSqlServer( configuration.GetConnectionString("DefaultConnection")));

        }
    }
}
