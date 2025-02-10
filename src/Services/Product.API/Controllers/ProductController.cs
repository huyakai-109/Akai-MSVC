using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Repositories.Interfaces;
using Shared.DTOs;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(
        IProductRepository productRepository,
        IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = mapper.Map<IEnumerable<ProductDto>>(await productRepository.GetAll());
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var product = await productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            var result = mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto request)
        {
            var productEnity = await productRepository.GetProductByNo(request.No);
            if(productEnity != null)
            {
                return BadRequest($"Product No {request.No} is existed.");
            }

            var product = mapper.Map<Entities.Product>(request);
            await productRepository.Create(product);
            await productRepository.SaveChangeAsync();

            var result = mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateProductDto request)
        {
            var product = await productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            product = mapper.Map(request, product);
            await productRepository.Update(product);
            await productRepository.SaveChangeAsync();

            var result = mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var product = await productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await productRepository.Delete(id);
            await productRepository.SaveChangeAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("{productNo}")]
        public async Task<IActionResult> Delete([FromRoute] string productNo)
        {
            var product = await productRepository.GetProductByNo(productNo);
            if (product == null)
            {
                return NotFound();
            }

            var result = mapper.Map<ProductDto>(product);
            return Ok(result);
        }
    }
}
