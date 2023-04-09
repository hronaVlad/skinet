using API.Infrastucture.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Infrastucture.Extensions
{
    public static class ApiBehaviorOptionsExtensions
    {
        public static IServiceCollection AddInvalidModelStateHandler(this IServiceCollection serivices)
        {
            serivices.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errros = actionContext.ModelState
                        .Where(_ => _.Value.Errors.Any())
                        .SelectMany(_ => _.Value.Errors)
                        .Select(_ => _.ErrorMessage);

                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = errros });
                };
            });

            return null;
        }
    }
}
