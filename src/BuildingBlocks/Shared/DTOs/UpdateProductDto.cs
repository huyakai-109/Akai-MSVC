namespace Shared.DTOs
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
