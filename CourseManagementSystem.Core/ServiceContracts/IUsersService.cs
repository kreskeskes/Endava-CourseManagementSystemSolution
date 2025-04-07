using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace CourseManagementSystem.Core.ServiceContracts
{
    public interface IUsersService
    {
        public Task<IdentityResult> RegisterAsync(RegisterRequestDTO registerRequest);

        public Task<IdentityResult> SignInAsync(LoginRequestDTO loginRequest);

        public Task<IdentityResult> SignOutAsync();

        public Task<IdentityUser> GetUserByEmail(string email);

        public Task<IdentityUser> ChangePasswordAsync(Guid userId, ChangePasswordRequets changePasswordRequets);

        public Task<List<IdentityUser>> GetUsers();

        public Task<IdentityUser> GetUserById(Guid userId);

        public Task<List<IdentityUser>> GetFilterSortUsers(UserFilterRequest filterRequest);

    }
}
