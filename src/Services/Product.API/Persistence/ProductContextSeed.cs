using ILogger = Serilog.ILogger;

namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
        {
            if (!productContext.Products.Any())
            {
                productContext.AddRange(GetProducts());
                await productContext.SaveChangesAsync();

                logger.Information("Seeded data for Product DB associated with context {DbContextName}", nameof(ProductContext));
            }
        }

        private static IEnumerable<Entities.Product> GetProducts()
        {
            return new List<Entities.Product>()
            {
                new ()
                {
                    No = "Lotus",
                    Name = "Esprit",
                    Description = "Description 1",
                    Price = 199.99m
                },

                new()
                {
                    No = "Lotus 2",
                    Name = "Esprit 2",
                    Description = "Description 2",
                    Price = 99.99m
                }
            };
        }
    }
}
