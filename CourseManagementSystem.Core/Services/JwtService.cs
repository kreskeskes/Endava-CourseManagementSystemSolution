using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CourseManagementSystem.Core.DTOs.User;
using CourseManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CourseManagementSystem.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenService _refreshTokenService;

        public JwtService(UserManager<IdentityUser<Guid>> userManager,
            IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters,
            IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<string> GenerateJwtTokenAsync(IdentityUser<Guid> user)
        {
            // get user role
            IList<string> roles = await _userManager.GetRolesAsync(user);

            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            //Data to include to payload
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //subject, unique identifier
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //Jwt token unique ID
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()), //issued at, time in seconds

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // for usage in API part
            };


            //adding role
            claims?.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //generating secret
            string secretKey = _configuration["Jwt:Key"];
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            //a component to sign the token to later check based on this signature whether the token is genuine or not


            //signing the token
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            //token generation

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
               _configuration["Jwt:Audience"],
               claims,
               signingCredentials: signingCredentials,
               expires: expiration);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }

        public async Task<TokenPairDTO> RefreshJwtTokenAsync(string? expiredAccessToken, string refreshToken, string ipAddress)
        {
            if (string.IsNullOrEmpty(expiredAccessToken))
            {
                throw new ArgumentException("The token is null");
            }

            var principal = GetPrincipalFromExpiredToken(expiredAccessToken);

            if (principal == null)
            {
                throw new UnauthorizedAccessException("Error while identifying principal");
            }

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out Guid validUserId))
            {
                throw new UnauthorizedAccessException("Invalid userId");
            }

            IdentityUser<Guid> user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }


            var foundRefreshToken = await _refreshTokenService.GetByTokenAsync(refreshToken);


            //if not already present refreshToken, revoke it, and throw Unuathorized exception
            if (foundRefreshToken == null || !foundRefreshToken.IsActive)
            {
                await _refreshTokenService.RevokeAsync(foundRefreshToken, ipAddress);
                throw new UnauthorizedAccessException("Invalid or expered refresh token.");
            }

            //generating a new refreshToken


            var newRefreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user.Id, ipAddress);

            string newAccessToken = await GenerateJwtTokenAsync(user);


            return new TokenPairDTO() { AccessTpken = newAccessToken, RefreshToken = newRefreshToken.Token };

        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var parameters = _tokenValidationParameters.Clone();
            parameters.ValidateLifetime = false;
            parameters.ValidateAudience = true;
            parameters.ValidateIssuer = true;
            parameters.ValidateIssuerSigningKey = true;


            var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);

            if (validatedToken is JwtSecurityToken jwt && jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
            {
                return principal;
            }

            return null;

        }
    }
}
