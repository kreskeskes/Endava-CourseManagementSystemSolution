using System.Security.Claims;
using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.API.ServiceContracts;
using CourseManagementSystem.Core.Constants;
using CourseManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            List<CourseResponse> courses = await _courseService.GetCourses();
            if (!courses.Any())
                return NotFound("No courses were found.");
            return Ok(courses);
        }

        [AllowAnonymous]
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
        [Authorize(Roles = $"{Roles.Admin},{Roles.Administrator}")]
        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseAddRequest courseAddRequest)
        {
            if (courseAddRequest == null)
                return BadRequest("Course add request cannot be empty.");

            var userId = GetCurrentUserId();
            courseAddRequest.CreatedBy = userId;
            CourseResponse? course = await _courseService.AddCourse(courseAddRequest);
            if (course == null)
                return StatusCode(500, "Error while adding course.");
            return Ok(course);
        }
        [Authorize]
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

        [Authorize(Roles = $"{Roles.Admin},{Roles.Administrator}")]
        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(Guid courseId, CourseUpdateRequest courseUpdateRequest)
        {
            if (courseId == Guid.Empty)
            {
                return BadRequest("Course id is null");
            }

            if (courseUpdateRequest == null)
                return BadRequest("Course update request cannot be empty.");

            if (courseUpdateRequest.Id != courseId)
            {
                return BadRequest("Course update request Id doesn't match course Id");
            }

            CourseResponse? course = await _courseService.UpdateCourse(courseUpdateRequest);
            if (course == null)
                return StatusCode(500, "Error while updating course.");
            return Ok(course);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Administrator}")]
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

        #region privateMethods
        private Guid GetCurrentUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new Exception("Couldn't identify current user.");
            return Guid.Parse(userId);
        }
        #endregion
    }


}
