using CourseManagementSystem.Core.Entities;
using CourseManagementSystem.Core.RepositoryContracts;
using CourseManagementSystem.Core.ServiceContracts;

namespace CourseManagementSystem.Core.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken?> CreateRefreshTokenAsync(Guid userId, string ipAddress)
        {
            return await _refreshTokenRepository.CreateRefreshTokenAsync(userId, ipAddress);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _refreshTokenRepository.GetByTokenAsync(token);
        }

        public async Task RevokeAsync(RefreshToken refreshToken, string ipToRevokeBy)
        {
            await _refreshTokenRepository.RevokeAsync(refreshToken, ipToRevokeBy);
        }
    }
}
