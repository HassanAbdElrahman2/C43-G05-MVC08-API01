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
        public ProductWithBrandsAndTypeSpecifications(ProductQueryPrams productQueryPrams) 
            :base(P=>(!productQueryPrams.BrandId.HasValue||P.BrandId == productQueryPrams.BrandId) 
            &&(!productQueryPrams.TypeId.HasValue||P.TypeId==productQueryPrams.TypeId)
            &&(String.IsNullOrWhiteSpace(productQueryPrams.SearchValue)||P.Name.ToLower().Contains(productQueryPrams.SearchValue.ToLower())))
        {
            AddIncludeExpressions(P => P.ProductBrand);
            AddIncludeExpressions(P => P.ProductType);
            switch (productQueryPrams.sortingOption)
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

            AplayPagination(productQueryPrams.PageIndex, productQueryPrams.PageSize);            
        }
        public ProductWithBrandsAndTypeSpecifications(int id) : base(P=>P.Id==id)
        {
            AddIncludeExpressions(P => P.ProductBrand);
            AddIncludeExpressions(P => P.ProductType);

        }
    }
}
