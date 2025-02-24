using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Shared.Common.Constants;

namespace Basket.API.Extensions
{
    public static class CoreDependencyConfiguration
    {
        public static void AddCacheService(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddScoped<IBasketRepository, BasketRepository>();

            collection.AddRedis(configuration);
        }

        private static void AddRedis(this IServiceCollection collection, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection(ConfigKeys.Databases.Redis).Value;
            collection.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = connectionString;
                options.InstanceName = CacheConstants.Redis.InstanceName;
            });
        }

        /* cần đăng ký Redis vào dependency injection container bằng cách sử dụng AddStackExchangeRedisCache trong phương thức mở rộng AddRedis,
         * vì IDistributedCache là một dịch vụ được cung cấp thông qua dependency injection (DI).
         */
    }
}
