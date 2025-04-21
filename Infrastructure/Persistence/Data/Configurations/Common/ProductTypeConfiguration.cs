using DomainLayer.Models.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations.Common
{
    internal class ProductTypeConfiguration :BaseConfiguration<int,ProductType>
    {
        public override void Configure(EntityTypeBuilder<ProductType> builder)
        {
            base.Configure(builder);
        }
    }
}
