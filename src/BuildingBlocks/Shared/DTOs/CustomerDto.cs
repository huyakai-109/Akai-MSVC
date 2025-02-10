using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public required string UserName { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public string? ImageUrl { get; set; }
    }
}
