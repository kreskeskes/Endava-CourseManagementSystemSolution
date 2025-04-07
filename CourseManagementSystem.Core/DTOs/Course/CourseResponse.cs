﻿using CourseManagementSystem.Core.Enums;

namespace CourseManagementSystem.API.DTOs.Course
{
    public class CourseResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Discipline Discipline { get; set; }
        public string Description { get; set; } = string.Empty;
        public Difficulty Difficulty { get; set; }

        public List<Guid> Contributors { get; set; } = new List<Guid>();
        public List<Guid> ModuleIds { get; set; } = new List<Guid>();

        public List<Guid> Enrollments { get; set; } = new List<Guid>();
    }
}
