using Microsoft.AspNetCore.Http;

namespace Shared.DTOs
{
    public class CreateUpdateCustomerDto
    {
        public required string UserName { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public IFormFile? ImageUrl { get; set; }
    }
}
