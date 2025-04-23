using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.Products;
using ServiceAbstraction;
using Shared.DataTranssferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {

           var Rpo= _unitOfWork.GetRepository<int, ProductBrand>();
           var Brands =await Rpo.GetAllAsync();
           var BrandsDTo = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTO>>(Brands);
            return BrandsDTo;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var Products = await _unitOfWork.GetRepository<int, Product>().GetAllAsync();
            var ProductsDto= _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products);
            return ProductsDto;
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types =await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var TypesDTO = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(Types);
            return TypesDTO;
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var Product=await _unitOfWork.GetRepository<int, Product>().GetByIdAsync(id);
            var ProductDto=_mapper.Map<Product,ProductDTO>(Product);
            return ProductDto;
        }
    }
}
