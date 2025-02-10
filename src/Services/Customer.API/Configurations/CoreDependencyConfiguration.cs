using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Minio;
using Shared.Common.Constants;

namespace Customer.API.Configurations
{
    public static class CoreDependencyConfiguration
    {
        public static void AddCoreDependencies(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddServices();
            collection.ConfigureCustomerDbContext(configuration);
            collection.AddMinIO(configuration);
        }

        private static void ConfigureCustomerDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection(ConfigKeys.Databases.MainDb_Cus).Value;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Database connection string is null");
            }

            services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));
        }

        private static void AddMinIO(this IServiceCollection collection, IConfiguration configuration)
        {
            var endpoint = configuration[ConfigKeys.MinIO.Endpoint];
            var accessKey = configuration[ConfigKeys.MinIO.AccessKey];
            var secretKey = configuration[ConfigKeys.MinIO.SecretKey];
            var region = configuration[ConfigKeys.MinIO.Region];
            var secure = configuration.GetValue<bool>(ConfigKeys.MinIO.Secure);
            collection.AddMinio(configureClient => configureClient
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .WithSSL(secure)
                //.WithRegion(region)
                .Build());
        }

        private static void AddServices(this IServiceCollection collection)
        {
            collection.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBase<,,>));
            collection.AddScoped(typeof(IRepositoryQueryBase<,,>), typeof(RepositoryQueryBase<,,>));
            collection.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            collection.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
