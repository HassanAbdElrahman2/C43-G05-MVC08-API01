using DomainLayer.Contracts;
using DomainLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDBContext _dBContext;

        public DbInitializer(StoreDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task IntializeAsync()
        {
            if((await _dBContext.Database.GetPendingMigrationsAsync()).Any())// Two Oprations
               await _dBContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {

            try
            {
                if (!(await _dBContext.ProductBrands.AnyAsync()))
                {
                    var BrandsStrings = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var Brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsStrings);
                    if (Brands is not null && Brands.Any())
                       await _dBContext.ProductBrands.AddRangeAsync(Brands);

                }

                if (!(await _dBContext.ProductTypes.AnyAsync()))
                {
                    var TypesStrings =File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var Types = await JsonSerializer.DeserializeAsync<List<ProductType>>(TypesStrings);
                    if (Types is not null && Types.Any())
                         await _dBContext.ProductTypes.AddRangeAsync(Types);
                }

                if (!await(_dBContext.Products.AnyAsync()))
                {
                    var ProductsStrings = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var Products =await JsonSerializer.DeserializeAsync<List<Product>>(ProductsStrings);
                    if (Products is not null && Products.Any())
                       await _dBContext.Products.AddRangeAsync(Products);
                }

                await _dBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
         

        }
    }
}
