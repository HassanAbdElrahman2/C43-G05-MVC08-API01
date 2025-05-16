using DomainLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductBrand> ProductBrands { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1-  modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());// must add all tables used it if i haven't many nambur of table 
           //  2-  modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // can not work must to in save used with it attribute

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }
    }
}
