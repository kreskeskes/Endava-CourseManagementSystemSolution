using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.API.ServiceContracts;
using CourseManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            List<CourseResponse> courses = await _courseService.GetCourses();
            if (!courses.Any())
                return NotFound("No courses were found.");
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById(Guid courseId)
        {
            if (courseId == Guid.Empty)
                return BadRequest("Course Id cannot be empty.");

            CourseResponse? course = await _courseService.GetCourseById(courseId);
            if (course == null)
                return NotFound("No course was found.");
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseAddRequest courseAddRequest)
        {
            if (courseAddRequest == null)
                return BadRequest("Course add request cannot be empty.");

            CourseResponse? course = await _courseService.AddCourse(courseAddRequest);
            if (course == null)
                return StatusCode(500, "Error while adding course.");
            return Ok(course);
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollToCourse(Guid courseId, Guid userId)
        {
            if (courseId == Guid.Empty)
                return BadRequest("Course Id cannot be empty.");

            if (userId == Guid.Empty)
                return BadRequest("User Id cannot be empty.");

            CourseResponse? course = await _courseService.EnrollUserToCourse(courseId, userId);
            if (course?.Enrollments == null && !course.Enrollments.Any(e => e == userId))
                return StatusCode(500, "Error while adding user to a course.");
            return Ok(course);
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(Guid courseId,CourseUpdateRequest courseUpdateRequest)
        {
            if (courseId == Guid.Empty)
            {
                return BadRequest("Course id is null");
            }

            if (courseUpdateRequest == null)
                return BadRequest("Course update request cannot be empty.");

            if (courseUpdateRequest.Id!=courseId)
            {
                return BadRequest("Course update request Id doesn't match course Id");
            }

            CourseResponse? course = await _courseService.UpdateCourse(courseUpdateRequest);
            if (course == null)
                return StatusCode(500, "Error while updating course.");
            return Ok(course);
        }

        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            if (courseId == Guid.Empty)
                return BadRequest("Course Id cannot be empty.");
            bool isDeletionSuccess = await _courseService.DeleteCourse(courseId);
            if (!isDeletionSuccess)
                return StatusCode(500, "Error while deleting course.");
            return Ok(isDeletionSuccess);
        }
    }
}
