using EFModels.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Infrastucture.Extensions
{
    public static class UseExtensions
    {
        public static async Task<AppUser> GetWithAddress(this ClaimsPrincipal principal, UserManager<AppUser> userManager)
        {
            var email = principal.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(_ => _.Address).FirstAsync(_ => _.Email == email);
        }

        public static async Task<AppUser> Get(this ClaimsPrincipal principal, UserManager<AppUser> userManager)
        {
            var email = principal.FindFirstValue(ClaimTypes.Email);

            return await userManager.FindByEmailAsync(email);
        }
    }
}
