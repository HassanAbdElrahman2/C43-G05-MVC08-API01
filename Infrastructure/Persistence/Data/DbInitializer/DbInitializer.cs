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
        public void Intialize()
        {
            if(_dBContext.Database.GetPendingMigrations().Any())
                _dBContext.Database.Migrate();
        }

        public void Seed()
        {

            try
            {
                if (!_dBContext.ProductBrands.Any())
                {
                    var BrandsStrings = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsStrings);
                    if (Brands is not null && Brands.Any())
                        _dBContext.ProductBrands.AddRange(Brands);

                }

                if (!_dBContext.ProductTypes.Any())
                {
                    var TypesStrings = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesStrings);
                    if (Types is not null && Types.Any())
                        _dBContext.ProductTypes.AddRange(Types);
                }

                if (!_dBContext.Products.Any())
                {
                    var ProductsStrings = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsStrings);
                    if (Products is not null && Products.Any())
                        _dBContext.Products.AddRange(Products);
                }

                _dBContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
         

        }
    }
}
