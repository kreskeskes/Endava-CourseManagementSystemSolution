using CourseManagementSystem.Core.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace CourseManagementSystem.Core.ServiceContracts
{
    public interface IUsersService
    {
        public Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO registerRequest);

        public Task<AuthResponseDTO> SignInAsync(LoginRequestDTO loginRequest);

        public Task<IdentityUser<Guid>?> GetUserByEmail(string email);

        public List<IdentityUser<Guid>> GetUsers();

        public Task<IdentityUser<Guid>?> GetUserById(Guid userId);


    }
}
