using Stripe;

namespace API.Infrastucture.Extensions
{
    public static class StripeExtensions
    {
        public static IServiceCollection AddStripeKey(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            StripeConfiguration.ApiKey = configuration["StripeCredentials:SecretKey"];

            return services;
        }
    }
}
