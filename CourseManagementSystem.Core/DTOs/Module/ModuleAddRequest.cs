﻿namespace CourseManagementSystem.API.DTOs
{
    public class ModuleAddRequest
    {
        public string Title { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public Guid CreatedBy { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Order { get; set; } 
    }
}
