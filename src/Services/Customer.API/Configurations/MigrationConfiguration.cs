using Customer.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Constants;


namespace Customer.API.Extensions
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
            var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();
            context.Database.Migrate();
        }
    }
}
