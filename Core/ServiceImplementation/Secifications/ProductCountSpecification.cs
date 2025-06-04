using DomainLayer.Models.Products;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Secifications
{
    internal class ProductCountSpecification: BaseSecification<Product,int>
    {
        public ProductCountSpecification(ProductQueryPrams productQueryPrams)
             : base(P=>(!productQueryPrams.BrandId.HasValue||P.BrandId == productQueryPrams.BrandId) 
            &&(!productQueryPrams.TypeId.HasValue||P.TypeId==productQueryPrams.TypeId)
            &&(String.IsNullOrWhiteSpace(productQueryPrams.search)||P.Name.ToLower().Contains(productQueryPrams.search.ToLower())))
        {
         
    }
}
}
