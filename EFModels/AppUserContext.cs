using EFModels.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFModels
{
    public class AppUserContext : IdentityDbContext<AppUser>
    {
        public AppUserContext(DbContextOptions options) : base(options)
        {
        }
    }
}
