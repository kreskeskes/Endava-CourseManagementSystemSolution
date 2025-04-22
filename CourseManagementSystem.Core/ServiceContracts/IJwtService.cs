using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CourseManagementSystem.Core.ServiceContracts
{
    public interface IJwtService
    {
        Task<string> GenerateJwtTokenAsync(IdentityUser<Guid> user);

        Task<TokenPairDTO> RefreshJwtTokenAsync(string? expiredAccessToken, string refreshToken, string ipAddress);
    }
}
