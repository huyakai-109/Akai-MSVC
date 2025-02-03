using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace Product.API.Entities
{
    [Table("Products")]
    public class Product : EntityAuditBase<long>
    {
        [Required]
        [MaxLength(50)]
        public required string No { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(250)")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
