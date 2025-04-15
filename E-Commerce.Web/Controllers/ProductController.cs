using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //BaseURL/api/Product/10
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id) {

            return new Product() { Id = id };
        }
        [HttpGet]
        public ActionResult<Product> GetAll()
        {

            return new Product() { Id = 100 };
        }
        [HttpPut]
        public ActionResult<Product> AddProduct(Product product)
        {

            return new Product() { Id = 100 };
        }
        [HttpPut]
        public ActionResult<Product> Update()
        {

            return new Product() { Id = 100 };
        }
    }
}
