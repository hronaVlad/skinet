using EFModels;
using EFModels.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Infrastucture.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppUserContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<SignInManager<AppUser>>();

            return services;
        }
    }
}
