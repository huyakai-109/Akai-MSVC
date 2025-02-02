using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.API.Persistence;
using Product.API.Repositories;
using Product.API.Repositories.Interfaces;
using Shared.Common.Constants;

namespace Product.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.ConfigureProductDbContext(configuration);
            services.AddInfrastructureServices();

            return services;
        }

        private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection(ConfigKeys.Databases.MainDb).Value;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Database connection string is null");
            }

            var builder = new MySqlConnectionStringBuilder(connectionString);

            services.AddDbContext<ProductContext>(options =>
                options.UseMySql(builder.ConnectionString, ServerVersion.AutoDetect(builder.ConnectionString), mySqlOptions =>
                {
                    mySqlOptions.MigrationsAssembly(GlobalConstants.Assembly.Product);
                    mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                })); ;

            return services;
        }

        private static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
