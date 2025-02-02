using Contracts.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await productRepository.GetAll();
            return Ok(result);
        }
    }
}
