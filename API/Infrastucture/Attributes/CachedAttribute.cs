using Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace API.Infrastucture.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expirtySeconds;

        public CachedAttribute(int expirtySeconds = 600)
        {
            _expirtySeconds = expirtySeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var key = GenerateCacheKey(context.HttpContext.Request);

            var cachedData = await cacheService.GetCachedResponseAsync(key);

            if (!string.IsNullOrEmpty(cachedData))
            {
                context.Result = new ContentResult
                {
                    Content = cachedData,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                return;
            }

            var executedActionContext = await next();

            if (executedActionContext.Result is OkObjectResult okResult)
            {
                await cacheService.CacheResponseAsync(key, okResult.Value, TimeSpan.FromSeconds(_expirtySeconds));
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path);

            foreach (var (key, value) in request.Query) {
                keyBuilder.Append($"{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
