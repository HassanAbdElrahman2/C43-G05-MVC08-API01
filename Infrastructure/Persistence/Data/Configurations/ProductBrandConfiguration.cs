using DomainLayer.Models.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Data.Configurations.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    internal class ProductBrandConfiguration :BaseConfiguration<int,ProductBrand>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);
        }
    }
}
