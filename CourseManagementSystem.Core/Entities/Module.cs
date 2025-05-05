namespace CourseManagementSystem.Core.Entities
{
    public class Module : BaseEntity
    {
        public Guid CourseId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Order { get; set; } //numerical order
        public Course Course { get; set; } = null!; //navigation property
    }
}
