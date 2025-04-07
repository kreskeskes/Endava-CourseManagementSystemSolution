using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.Core.Entities;
using CourseManagementSystem.Core.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Infrastructure.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly ApplicationDbContext _db;

        public CoursesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Course?> AddCourse(Course course)
        {
            if (course == null)
                return null;
            course.CreatedAt = DateTime.UtcNow;
            course.UpdatedAt = DateTime.UtcNow;
            await _db.Courses.AddAsync(course);
            await _db.SaveChangesAsync();
            return course;
        }

        public async Task<bool> DeleteCourse(Guid courseId)
        {
            Course? foundCourse = await _db.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (foundCourse == null)
                return false;
            _db.Courses.Remove(foundCourse);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<Course> EnrollUserToCourse(Guid courseId, Guid userId)
        {
            if (userId == Guid.Empty)
                return null;
            Course? course = await _db.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            course.Enrollments.Add(userId);
            await _db.SaveChangesAsync();

            return course;
        }

        public async Task<Course?> GetCourseById(Guid courseId)
        {
            Course? course = await _db.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
                return null;
            return course;
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _db.Courses.ToListAsync();
        }

        public async Task<Course?> UpdateCourse(Course course)
        {
            Course foundCourse = await _db.Courses.FirstOrDefaultAsync(c => c.Id == course.Id);
            if (foundCourse == null)
                return null;


            if (!course.ModuleIds.Any())
            {
                _db.Courses.Remove(foundCourse);
                await _db.SaveChangesAsync();
                return null;
            }

            foundCourse.UpdatedAt = DateTime.UtcNow;

            foundCourse.Description = course.Description;

            foundCourse.Contributors = course.Contributors ?? new List<Guid>();
            foundCourse.Enrollments = course.Enrollments ?? new List<Guid>();

            foundCourse.ModuleIds = course.ModuleIds ?? new List<Guid>();

            foundCourse.Difficulty = course.Difficulty;
            foundCourse.Title = course.Title;
            foundCourse.CreatedBy = course.CreatedBy;
            foundCourse.Discipline = course.Discipline;


            await _db.SaveChangesAsync();

            return foundCourse;

        }
    }
}
