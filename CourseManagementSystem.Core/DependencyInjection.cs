using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.API.ServiceContracts;
using CourseManagementSystem.Core.Mappers;
using CourseManagementSystem.Core.RepositoryContracts;
using CourseManagementSystem.Core.Services;
using CourseManagementSystem.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagementSystem.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CourseAddRequestToCourseMappingProfile).Assembly);
            services.AddValidatorsFromAssemblyContaining(typeof(ModuleAddRequestValidator));
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IModuleService, ModuleService>();
            return services;
        }
    }
}
