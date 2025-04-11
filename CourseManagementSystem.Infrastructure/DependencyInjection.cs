using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.RepositoryContracts;
using CourseManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUserConfirmation<IdentityUser<Guid>>, DefaultUserConfirmation<IdentityUser<Guid>>>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser<Guid>>, UserClaimsPrincipalFactory<IdentityUser<Guid>, IdentityRole<Guid>>>();
            services.AddScoped<SignInManager<IdentityUser<Guid>>>();
            //resgitering only the Identity components we need, without Auth Cookies
            services.AddScoped<UserManager<IdentityUser<Guid>>>();
            services.AddScoped<RoleManager<IdentityRole<Guid>>>();
            services.AddScoped<IUserStore<IdentityUser<Guid>>, UserStore<IdentityUser<Guid>, IdentityRole<Guid>, ApplicationDbContext, Guid>>();
            services.AddScoped<IRoleStore<IdentityRole<Guid>>, RoleStore<IdentityRole<Guid>, ApplicationDbContext, Guid>>();

            //For future need to reset password/confirm email
            services.AddScoped<IPasswordHasher<IdentityUser<Guid>>, PasswordHasher<IdentityUser<Guid>>>();
            services.AddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.AddScoped<IdentityErrorDescriber>();

            services.AddScoped<ICoursesRepository, CoursesRepository>();
            services.AddScoped<IModulesRepository, ModulesRepository>();

            return services;
        }
    }
}
