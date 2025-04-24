using DomainLayer.Models.Products;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Secifications
{
    internal class ProductWithBrandsAndTypeSpecifications :BaseSecification<Product,int>
    {
        public ProductWithBrandsAndTypeSpecifications(int? BrandId, int? TypeId, ProductSortingOptions sortingOption) 
            :base(P=>(!BrandId.HasValue||P.BrandId ==BrandId)&&(!TypeId.HasValue||P.TypeId==TypeId))
        {
            AddIncludeExpressions(P => P.ProductBrand);
            AddIncludeExpressions(P => P.ProductType);
            switch (sortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
            }

            
        }
        public ProductWithBrandsAndTypeSpecifications(int id) : base(P=>P.Id==id)
        {
            AddIncludeExpressions(P => P.ProductBrand);
            AddIncludeExpressions(P => P.ProductType);

        }
    }
}
