using Contracts.Domains;
using System.ComponentModel.DataAnnotations;

namespace Customer.API.Entities
{
    public class Customer : EntityAuditBase<int>
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public string? ImageUrl { get; set; }
    }
}
