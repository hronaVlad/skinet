using Core.Repositories.Contracts;
using Models.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace Core.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket> GetAsync(string id)
        {
            RedisValue data = await _database.StringGetAsync(id);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return new CustomerBasket(id)
                {
                    Items = JsonSerializer.Deserialize<List<BasketItem>>(data)
                };
        }

        public async Task<CustomerBasket> UpdateAsync(CustomerBasket customerBasket)
        {
             var created = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket.Items), TimeSpan.FromDays(30));

            if (!created)
            {
                return null;
            }

            return await GetAsync(customerBasket.Id);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}
