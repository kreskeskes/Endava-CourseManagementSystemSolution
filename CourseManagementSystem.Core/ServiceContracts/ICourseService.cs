using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.API.DTOs.Course;

namespace CourseManagementSystem.API.ServiceContracts
{
    public interface ICourseService
    {
        Task<CourseResponse> AddCourse(CourseAddRequest courseAddRequest);
        Task<CourseResponse> DeleteCourse(Guid courseId);
        Task<CourseResponse> UpdateCourse(CourseUpdateRequest courseUpdateRequest);
        Task<List<CourseResponse>> GetCourses();
        Task<CourseResponse> GetCourseById(Guid courseId);
        Task<CourseResponse> EnrollUserToCourse(Guid userId);

    }
}
