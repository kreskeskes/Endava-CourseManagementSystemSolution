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
        public Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO registerRequest);

        public Task<AuthResponseDTO> SignInAsync(LoginRequestDTO loginRequest);

        public Task<bool> SignOutAsync();

        public Task<IdentityUser<Guid>?> GetUserByEmail(string email);

        public List<IdentityUser<Guid>> GetUsers();

        public Task<IdentityUser<Guid>?> GetUserById(Guid userId);


    }
}
