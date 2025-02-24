using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basket.API.Entities
{
    public class BasketCheckout
    {
        [Required]
        public required string UserName { get; set; }

        [Column(TypeName = "decimal(19,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
