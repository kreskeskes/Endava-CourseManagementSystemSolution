using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagementSystem.Infrastructure.SeedIdentity
{
    public static class SeedIdentity
    {
        private static string[] Roles = { "Admin", "Administrator", "User" };
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }
            foreach (var role in Roles )
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    var results = await roleManager.CreateAsync(new IdentityRole(role));

                    if (!results.Succeeded)
                    {
                        throw new Exception("Error while creating role.");
                    }
                }
            }
        }
    }
}
