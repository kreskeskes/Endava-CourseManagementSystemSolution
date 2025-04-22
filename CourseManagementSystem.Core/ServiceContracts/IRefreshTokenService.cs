using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.Entities;

namespace CourseManagementSystem.Core.ServiceContracts
{
    public interface IRefreshTokenService
    {
        public Task<RefreshToken?> CreateRefreshTokenAsync(Guid userId, string ipAddress);
        public Task<RefreshToken?> GetByTokenAsync(string token);
        public Task RevokeAsync(RefreshToken refreshToken, string ipToRevokeBy);
    }
}
