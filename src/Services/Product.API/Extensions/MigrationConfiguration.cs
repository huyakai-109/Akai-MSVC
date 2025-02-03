using Microsoft.EntityFrameworkCore;
using Product.API.Persistence;
using Shared.Common.Constants;

namespace Product.API.Extensions
{
    public static class MigrationConfiguration
    {
        public static void RunMigration(this WebApplication app)
        {
            var autoMigration = app.Configuration.GetSection(ConfigKeys.AutoMigration).Get<bool>();
            if (!autoMigration)
            {
                return;
            }

            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
            context.Database.Migrate();
        }
    }
}
