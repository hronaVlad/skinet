using API.Infrastucture.Errors;
using Core.Repositories;
using Core.Repositories.Contracts;
using Core.Services;
using Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Infrastucture.Extensions
{
    public static class CollectionServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();


            return services;
        }
    }
}