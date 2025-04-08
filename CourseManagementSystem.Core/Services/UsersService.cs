using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public UsersService(SignInManager<IdentityUser<Guid>> signInManager, UserManager<IdentityUser<Guid>> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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
            return _userManager.Users.ToList();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequestDTO registerRequest)
        {
            IdentityUser<Guid> user = new IdentityUser<Guid>()
            {
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.Phonenumber,
                UserName = registerRequest.UserName
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true);
            }
            return result;
        }

        public async Task<SignInResult> SignInAsync(LoginRequestDTO loginRequest)
        {
            IdentityUser<Guid>? user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null)
            {
                return await _signInManager.PasswordSignInAsync(user, loginRequest.Password, isPersistent: true, lockoutOnFailure: false);
            }
            return SignInResult.Failed;
        }

        public async Task<bool> SignOutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
