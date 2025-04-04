using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.API.ServiceContracts;

namespace CourseManagementSystem.Core.Services
{
    public class CourseService : ICourseService
    {
        public Task<CourseResponse> AddCourse(ModuleAddRequest moduleAddRequest)
        {
            throw new NotImplementedException();
        }

        public Task<CourseResponse> DeleteCourse(Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<CourseResponse> EnrollUserToCourse(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<CourseResponse> GetCourseById(Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseResponse>> GetCourses()
        {
            throw new NotImplementedException();
        }

        public Task<CourseResponse> UpdateCourse(ModuleUpdateRequest moduleUpdateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
