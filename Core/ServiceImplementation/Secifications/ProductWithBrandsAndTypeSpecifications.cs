using DomainLayer.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Secifications
{
    internal class ProductWithBrandsAndTypeSpecifications :BaseSecification<Product,int>
    {
        public ProductWithBrandsAndTypeSpecifications():base(null)
        {
            AddIncludeExpressions(P => P.ProductBrand);
            AddIncludeExpressions(P => P.ProductType);
            
        }
        public ProductWithBrandsAndTypeSpecifications(int id) : base(P=>P.Id==id)
        {
            AddIncludeExpressions(P => P.ProductBrand);
            AddIncludeExpressions(P => P.ProductType);

        }
    }
}
