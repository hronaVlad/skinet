using API.Services;
using Core.Repositories;
using Core.Repositories.Contracts;
using Core.Services;
using Core.Services.Contracts;

namespace API.Infrastucture.Extensions
{
    public static class CollectionServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentService, StripePaymentService>();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();

            return services;
        }
    }
}