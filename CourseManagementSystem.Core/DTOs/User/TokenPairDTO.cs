namespace CourseManagementSystem.Core.DTOs.User
{
    public class TokenPairDTO
    {
        public string AccessTpken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
