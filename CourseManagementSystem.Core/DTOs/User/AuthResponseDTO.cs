namespace CourseManagementSystem.Core.DTOs.User
{
    public class AuthResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public bool Success { get; set; } = false;
    }
}
