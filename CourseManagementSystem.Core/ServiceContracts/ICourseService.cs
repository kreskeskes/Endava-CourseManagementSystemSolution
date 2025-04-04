using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.API.DTOs.Course;

namespace CourseManagementSystem.API.ServiceContracts
{
    public interface ICourseService
    {
        Task<CourseResponse> AddCourse(ModuleAddRequest moduleAddRequest);
        Task<CourseResponse> DeleteCourse(Guid moduleId);
        Task<CourseResponse> UpdateCourse(ModuleUpdateRequest moduleUpdateRequest);
        Task<List<CourseResponse>> GetCourses();
        Task<CourseResponse> GetCourseById(Guid moduleId);
        Task<CourseResponse> EnrollUserToCourse(Guid userId);

    }
}
