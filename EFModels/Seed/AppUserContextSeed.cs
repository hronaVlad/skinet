﻿using EFModels.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EFModels.Seed
{
    public class AppUserContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, ILoggerFactory loggerFactory)
        {

            var logger = loggerFactory.CreateLogger<AppUserContextSeed>();

            try
            {
                if (!userManager.Users.Any())
                {
                    var admin = new AppUser
                    {
                        DisplayName = "admin",
                        Email = "admin@admin.com",
                        UserName = "admin@admin.com",
                        Address = new Address
                        {
                            FirstName = "admin",
                            LastName = "admin",
                            City = "New York",
                            State = "NY",
                            Street = "10 The Updated Street",
                            PostalCode = "90250"
                        }
                    };

                    var result = await userManager.CreateAsync(admin, password: "Pa$$w0rd");

                    if (!result.Succeeded)
                    {

                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occured during data seed");
                throw;
            }
        }
    }
}
