using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.Core.Entities;

namespace CourseManagementSystem.Core.RepositoryContracts
{
    public interface ICoursesRepository
    {
        Task<Course> AddCourse(Course course);
        Task<bool> DeleteCourse(Guid moduleId);
        Task<Course> UpdateCourse(Course course);
        Task<List<Course>> GetCourses();
        Task<Course> GetCourseById(Guid courseId);
        Task<Course> EnrollUserToCourse(Guid userId);
    }
}
