using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.API.ServiceContracts;
using CourseManagementSystem.Core.Mappers;
using CourseManagementSystem.Core.RepositoryContracts;
using CourseManagementSystem.Core.ServiceContracts;
using CourseManagementSystem.Core.Services;
using CourseManagementSystem.Core.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagementSystem.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CourseAddRequestToCourseMappingProfile).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(typeof(ModuleAddRequestValidator));
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
