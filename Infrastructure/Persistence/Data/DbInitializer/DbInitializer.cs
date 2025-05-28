using DomainLayer.Contracts;
using DomainLayer.Models.Identity;
using DomainLayer.Models.Orders;
using DomainLayer.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StoreIdentityDbContext _IdentitydbContext;

        public DbInitializer(StoreDBContext dBContext
            ,UserManager<ApplicationUser> userManager
            ,RoleManager<IdentityRole> roleManager,
            StoreIdentityDbContext dbContext)
        {
            _dBContext = dBContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _IdentitydbContext = dbContext;
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
                if (!await (_dBContext.Set<DeliveryMethod>().AnyAsync()))
                {
                    var deliveryStrings = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");
                    var deliveries = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(deliveryStrings);
                    if (deliveries is not null && deliveries.Any())
                        await _dBContext.Set<DeliveryMethod>().AddRangeAsync(deliveries);
                }

                await _dBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
         

        }
        public async Task IdentitySeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DispalayName = "Mohamed Tarek",
                        PhoneNumber = "0123456789",
                        UserName = "MohamedTarek"
                    };

                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(User01, "Admin");
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DispalayName = "Salma Mohamed",
                        PhoneNumber = "0123456789",
                        UserName = "SalmaMohamed"
                    };
                    await _userManager.CreateAsync(User02, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(User01, "SuperAdmin");
                }

                await _IdentitydbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                
            }

        }
    }
}
