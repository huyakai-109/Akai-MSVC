using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository(
        IDistributedCache distributedCache,
        ILogger<BasketRepository> logger) : IBasketRepository
    {
        public async Task<bool> DeleteBasketFromUserName(string userName)
        {
            try
            {
                await distributedCache.RemoveAsync(userName);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Cart?> GetBasketByUserName(string userName)
        {
            var jsonData = await distributedCache.GetStringAsync(userName);
            if (jsonData == null)
                return default;

            return JsonConvert.DeserializeObject<Cart>(jsonData);
        }

        public async Task<Cart> UpdateBasket(Cart cart, TimeSpan? cacheDuration)
        {
            var jsonData = JsonConvert.SerializeObject(cart);

            if (cacheDuration != null)
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheDuration
                };
                await distributedCache.SetStringAsync(cart.UserName, jsonData, options);
            }
            else
            {
                await distributedCache.SetStringAsync(cart.UserName, jsonData);
            }

            return await GetBasketByUserName(cart.UserName) ?? cart;
        }
    }
}
