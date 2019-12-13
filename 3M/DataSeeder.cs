using _3M.Constants;
using _3M.DataModels;
using _3M.DbContexts;
using _3M.Dtos.Account;
using _3M.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _3M
{
    public static class DataSeeder
    {
        public static void SeedAuthData(IApplicationBuilder app)
        {
            SeedRoles(app);
            SeedUsers(app);
        }

        public static void SeedRoles(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var manager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>())
                {
                    if (manager.Roles.Any())
                        return;

                    var roles = typeof(RoleConstants).GetFields(BindingFlags.Static | BindingFlags.Public)
                        .Select(i => i.GetValue(null)).OfType<string>();

                    foreach (var role in roles)
                        manager.CreateAsync(new IdentityRole<Guid>(role)).Wait();
                }
            }
        }

        public static void SeedUsers(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var manager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>())
                {
                    if (manager.Users.Any())
                        return;

                    var userProfileRepository = scope.ServiceProvider.GetRequiredService<UserprofileRepository>();

                    var admin = new AppUser()
                    {
                        Id = Guid.NewGuid(),
                        Email = "mmm@Shop.ir",
                        UserName = "admin"
                    };
                    manager.CreateAsync(admin, "12345").Wait();
                    manager.AddToRoleAsync(admin, RoleConstants.Admin).Wait();
                    manager.AddClaimAsync(admin, new Claim(ClaimContants.FullName, "mmm")).Wait();
                    userProfileRepository.RegisterUserProfileAsync(new UserProfileDto()
                    {
                        UserId = admin.Id,
                        FirstName = "محمد مهدی",
                        LastName = "میروکیلی",
                    }).Wait();
                    var user = new AppUser()
                    {
                        Id = Guid.NewGuid(),
                        Email = "user@Shop.ir",
                        UserName = "user"
                    };
                    manager.CreateAsync(user, "12345").Wait();
                    manager.AddToRoleAsync(user, RoleConstants.User).Wait();
                    manager.AddClaimAsync(user, new Claim(ClaimContants.FullName, "حسین رضوانی")).Wait();
                    userProfileRepository.RegisterUserProfileAsync(new UserProfileDto()
                    {
                        UserId = user.Id,
                        FirstName = "حسین",
                        LastName = "رضوانی",
                    }).Wait();
                }
            }
        }
    }
}
