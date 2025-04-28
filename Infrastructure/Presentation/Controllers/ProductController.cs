using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTranssferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProdcuts([FromQuery]ProductQueryPrams productQueryPrams)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(productQueryPrams);
            return Ok(Products);
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int Id )
        {
            var Product = await _serviceManager.ProductService.GetProductById(Id);
            return Ok(Product);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypesAsync()
        {
            var Types= await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrandsAsync()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }

    }
}
