using API.Infrastucture.Errors;
using Core.Repositories;
using Core.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Infrastucture.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

            //builder.Services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errros = actionContext.ModelState
                        .Where(_ => _.Value.Errors.Any())
                        .SelectMany(_ => _.Value.Errors)
                        .Select(_ => _.ErrorMessage);

                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = errros});
                };
            });
            return services;
        }
    }
}