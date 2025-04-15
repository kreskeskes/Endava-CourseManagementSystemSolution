using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.Constants;
using CourseManagementSystem.Core.DTOs.User;
using CourseManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace CourseManagementSystem.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IJwtService _jwtService;


        public UsersService(
            SignInManager<IdentityUser<Guid>> signInManager,
            UserManager<IdentityUser<Guid>> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        public async Task<IdentityUser<Guid>?> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityUser<Guid>?> GetUserById(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public List<IdentityUser<Guid>> GetUsers()
        {
            Console.WriteLine(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            return _userManager.Users.ToList();
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO registerRequest)
        {
            IdentityUser<Guid> user = new IdentityUser<Guid>()
            {
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.Phonenumber,
                UserName = registerRequest.UserName
            };

            var duplicateUser = await _userManager.FindByEmailAsync(user.Email);
            if (duplicateUser != null)
            {
                throw new Exception("A user with such email has already been registered.");

            }

            IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);

                await _signInManager.PasswordSignInAsync(user, registerRequest.Password, false, false);
                AuthResponseDTO authResponseDTO = new AuthResponseDTO()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                    Token = await _jwtService.GenerateJwtTokenAsync(user),
                    Success = true
                };

                return authResponseDTO;

            }
            return new AuthResponseDTO() { Success = false };

        }

        public async Task<AuthResponseDTO> SignInAsync(LoginRequestDTO loginRequest)
        {
            IdentityUser<Guid>? user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return new AuthResponseDTO() { Success = false };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                AuthResponseDTO authResponseDTO = new AuthResponseDTO()
                {
                    Email = user.Email,
                    UserId = user.Id,
                    Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                    Token = await _jwtService.GenerateJwtTokenAsync(user),
                    Success = true
                };
                return authResponseDTO;
            }

            return new AuthResponseDTO { Success = false };
        }
    }
}
