using API.Mappers;
using System.Reflection;

namespace API.Infrastucture.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddAutoMapper(cfg => {
                cfg.AddProfile(new MappingProfiles(configuration));
            });
        }
    }
}
