using System.Net;
using System.Text.Json;

namespace API.Infrastucture.Errors.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly IHostEnvironment _env;
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleware> _logger;
        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger, IHostEnvironment env)
        {
            this._logger = logger;
            this._next = next;
            this._env = env;
        }

        public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context) {
            try {
                await _next(context);
            }catch(Exception e) {
                _logger.LogError(e, e.Message);

                var statusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var isDevelopment = _env.IsDevelopment();
                var response = isDevelopment
                    ? new ApiException(statusCode, e.Message, e.StackTrace.ToString())
                    : new ApiException(statusCode, e.Message);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options:options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}