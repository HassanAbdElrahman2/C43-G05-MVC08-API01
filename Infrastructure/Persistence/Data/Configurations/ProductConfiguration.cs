using DomainLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data.Configurations.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    class ProductConfiguration : BaseConfiguration<int, Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.HasOne(P => P.ProductBrand)
           .WithMany()
           .HasForeignKey(P => P.BrandId);

            builder.HasOne(P => P.ProductType)
                .WithMany()
                .HasForeignKey(P => P.TypeId);

            builder.Property(P => P.Price)
                .HasColumnType("decimal(10,2)");
        }

    }
}
