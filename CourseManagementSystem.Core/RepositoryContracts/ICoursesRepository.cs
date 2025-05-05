using CourseManagementSystem.Core.Entities;

namespace CourseManagementSystem.Core.RepositoryContracts
{
    public interface ICoursesRepository
    {
        Task<Course?> AddCourse(Course course);
        Task<bool> DeleteCourse(Guid courseId);
        Task<Course?> UpdateCourse(Course course);
        Task<List<Course>> GetCourses();
        Task<Course?> GetCourseById(Guid courseId);
        Task<Course> EnrollUserToCourse(Guid courseId, Guid userId);
    }
}
