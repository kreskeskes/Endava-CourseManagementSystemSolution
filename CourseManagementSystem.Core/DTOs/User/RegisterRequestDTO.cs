namespace CourseManagementSystem.Core.DTOs.User
{
    public class RegisterRequestDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? UserName { get; set; }
        public string? Phonenumber { get; set; }

    }
}
