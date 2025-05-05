using CourseManagementSystem.Core.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace CourseManagementSystem.Core.ServiceContracts
{
    public interface IJwtService
    {
        Task<string> GenerateJwtTokenAsync(IdentityUser<Guid> user);

        Task<TokenPairDTO> RefreshJwtTokenAsync(string? expiredAccessToken, string refreshToken, string ipAddress);
    }
}
