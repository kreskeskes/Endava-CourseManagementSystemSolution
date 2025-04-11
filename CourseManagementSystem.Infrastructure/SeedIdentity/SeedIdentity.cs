using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagementSystem.Infrastructure.SeedIdentity
{
    public static class SeedIdentity
    {
        private static string[] RoleList = { Roles.Admin, Roles.Administrator, Roles.User };
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }
            foreach (var role in RoleList)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    var results = await roleManager.CreateAsync(new IdentityRole<Guid>(role));

                    if (!results.Succeeded)
                    {
                        throw new Exception("Error while creating role.");
                    }
                }
            }

            if (roleManager.Roles.Any())
            {
                await SeedAdministratorAsync(serviceProvider);
            }
        }

        private static async Task SeedAdministratorAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser<Guid>>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();


            if (userManager == null)
            {
                throw new ArgumentException(nameof(userManager));
            }

            var users = userManager.Users.ToList();
            bool administratorExists = false;

            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, Roles.Administrator))
                {
                    administratorExists = true;
                    break;
                  
                }
            }
            if (!administratorExists)
            {
                var administractorUser = new IdentityUser<Guid>()
                {
                    Email = "administrator@example.com",
                    UserName = "administrator@example.com"
                };
                string administratorPassword = "AdminPassword123!";

                var result = await userManager.CreateAsync(administractorUser, administratorPassword);
                if (!result.Succeeded)
                {
                    throw new Exception("Error while seeding admin");
                }

              await  userManager.AddToRoleAsync(administractorUser, Roles.Administrator);
            }
        }
    }
}
