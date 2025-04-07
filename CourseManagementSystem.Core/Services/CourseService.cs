using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.API.ServiceContracts;
using CourseManagementSystem.Core.Entities;
using CourseManagementSystem.Core.RepositoryContracts;

namespace CourseManagementSystem.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IMapper _mapper;

        public CourseService(ICoursesRepository coursesRepository, IMapper mapper)
        {
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        public async Task<CourseResponse?> AddCourse(CourseAddRequest courseAddRequest)
        {
            Course course = _mapper.Map<Course>(courseAddRequest);

            course.Id = Guid.NewGuid();
            course.CreatedAt = DateTime.UtcNow;
            course.UpdatedAt = DateTime.UtcNow;

            Course? addedCourse = await _coursesRepository.AddCourse(course);
            if (addedCourse != null)
            {
                return _mapper.Map<CourseResponse>(addedCourse);
            }
            return null;
        }

        public async Task<bool> DeleteCourse(Guid courseId)
        {
            return await _coursesRepository.DeleteCourse(courseId);
        }

        public async Task<CourseResponse> EnrollUserToCourse(Guid courseId, Guid userId)
        {
            return _mapper.Map<CourseResponse>(await _coursesRepository.EnrollUserToCourse(courseId, userId));
        }

        public async Task<CourseResponse?> GetCourseById(Guid courseId)
        {
            Course? course = await _coursesRepository.GetCourseById(courseId);
            if (course == null)
                return null;

            return _mapper.Map<CourseResponse>(course);

        }

        public async Task<List<CourseResponse>> GetCourses()
        {
            return _mapper.Map<List<CourseResponse>>(await _coursesRepository.GetCourses());
        }

        public async Task<CourseResponse> UpdateCourse(CourseUpdateRequest courseUpdateRequest)
        {
            Course course = _mapper.Map<Course>(courseUpdateRequest);
            return _mapper.Map<CourseResponse>(await _coursesRepository.UpdateCourse(course));
        }
    }
}
