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
    internal class CourseService : ICourseService
    {
        public Task<CourseResponse> AddCourse(CourseAddRequest courseAddRequest)
        {
            throw new NotImplementedException();
        }

        public Task<CourseResponse> DeleteCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public Task<CourseResponse> EnrollUserToCourse(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<CourseResponse> GetCourseById(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseResponse>> GetCourses()
        {
            throw new NotImplementedException();
        }

        public Task<CourseResponse> UpdateCourse(CourseUpdateRequest courseUpdateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
