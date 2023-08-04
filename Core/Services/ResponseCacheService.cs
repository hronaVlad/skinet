using Core.Services.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace Core.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private IDatabase _cache;

        public ResponseCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _cache = connectionMultiplexer.GetDatabase();
        }

        public async Task CacheResponseAsync(string key, object value, TimeSpan timeToLive)
        {
            var serializedValue =  JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await this._cache.StringSetAsync(key, serializedValue, timeToLive);
        }

        public async Task<string> GetCachedResponseAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }
    }
}
