namespace Core.Services.Contracts
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string key, object value, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string key);
    }
}
