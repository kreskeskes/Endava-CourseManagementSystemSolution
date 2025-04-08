using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        public JwtService(UserManager<IdentityUser<Guid>> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtToken(IdentityUser<Guid> user)
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
    }
}
