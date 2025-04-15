using CourseManagementSystem.Core.Enums;

namespace CourseManagementSystem.Core.Entities
{
    public class Course : BaseEntity
    {  
        public Discipline Discipline { get; set; }
        public Difficulty Difficulty { get; set; }
        public List<Guid> Contributors { get; set; } = [];
        public List<Guid> ModuleIds { get; set; } = [];
        public List<Guid> Enrollments { get; set; } = [];
        public List<Module> Modules { get; set; } = null!; //navigation property

    }
}
