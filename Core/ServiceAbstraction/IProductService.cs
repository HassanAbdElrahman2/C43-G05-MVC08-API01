using Shared.DataTranssferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
   public interface IProductService
    {
       Task<IEnumerable<ProductDTO>> GetAllProductsAsync(int? BrandId, int? TypeId);
       Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
       Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
       Task<ProductDTO> GetProductById(int id);
    }
}
