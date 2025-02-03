namespace Shared.DTOs
{
    public class CreateProductDto
    {
        public required string No { get; set; }

        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
