using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagementSystem.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CourseAddRequestToCourseMappingProfile).Assembly);
            return services;
        }
    }
}
