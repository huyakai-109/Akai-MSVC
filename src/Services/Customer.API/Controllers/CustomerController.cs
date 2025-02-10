using AutoMapper;
using Customer.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Constants;
using Shared.DTOs;

namespace Customer.API.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController(
        ICustomerRepository customerRepository,
        IMapper mapper,
        IConfiguration configuration) : ControllerBase
    {
        private readonly string _storage = configuration[ConfigKeys.StorageUrl] ?? string.Empty;

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = mapper.Map<IEnumerable<CustomerDto>>(await customerRepository.GetAll());
            foreach (var item in result)
            {
                item.ImageUrl = GetFile(item.ImageUrl!, _storage);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            var result = mapper.Map<CustomerDto>(customer);
            result.ImageUrl = !string.IsNullOrEmpty(result.ImageUrl) ? GetFile(result.ImageUrl, _storage) : string.Empty;
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUpdateCustomerDto request)
        {
            var customerEntity = await customerRepository.GetCustomerByUserName(request.UserName);
            if (customerEntity != null)
            {
                return BadRequest($"Customer's user name {request.UserName} is existed.");
            }

            var customer = mapper.Map<Entities.Customer>(request);
            await customerRepository.Create(customer);
            await customerRepository.SaveChangeAsync();

            var result = mapper.Map<CustomerDto>(customer);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] CreateUpdateCustomerDto request)
        {
            var customer = await customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer = mapper.Map(request, customer);
            await customerRepository.Update(customer);
            await customerRepository.SaveChangeAsync();

            var result = mapper.Map<CustomerDto>(customer);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var customer = await customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            await customerRepository.Delete(id);
            await customerRepository.SaveChangeAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("{userName}")]
        public async Task<IActionResult> Delete([FromRoute] string userName)
        {
            var customer = await customerRepository.GetCustomerByUserName(userName);
            if (customer == null)
            {
                return NotFound();
            }

            var result = mapper.Map<CustomerDto>(customer);
            return Ok(result);
        }

        private string GetFile(string fileName, string url)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }

            return string.Concat(url, fileName);
        }
    }
}
