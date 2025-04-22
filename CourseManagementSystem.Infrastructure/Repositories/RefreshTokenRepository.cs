using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.Entities;
using CourseManagementSystem.Core.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _db;

        public RefreshTokenRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
           RefreshToken? refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            return refreshToken;
        }

        public async Task<RefreshToken?> CreateRefreshTokenAsync(Guid userId, string ipAddress)
        {
            var token = new RefreshToken()
            {
                UserId = userId,
                CreatedByIp = ipAddress,
                Expires = DateTime.UtcNow.AddDays(7),
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.UtcNow,
            };

            _db.RefreshTokens.Add(token);
            await _db.SaveChangesAsync();

            return token;
        }

        public async Task RevokeAsync(RefreshToken refreshToken, string ipToRevokeBy)
        {
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipToRevokeBy;

            await _db.SaveChangesAsync();

        }
    }
}
