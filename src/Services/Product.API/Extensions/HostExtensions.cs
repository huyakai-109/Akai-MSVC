using Microsoft.EntityFrameworkCore;

namespace Product.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetRequiredService<TContext>();
                try
                {
                    logger.LogInformation(message: "Migrating mysql database.");
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, message: "An error occurred while migrating the mysql database");
                }
            }
            return host;
        }
    }
}
