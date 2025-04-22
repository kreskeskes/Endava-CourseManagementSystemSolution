using System.Security.Claims;
using CourseManagementSystem.Core.DTOs.User;
using CourseManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IJwtService _jwtService;

        public UsersController(IUsersService usersService, IJwtService jwtService)
        {
            _usersService = usersService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<IdentityUser<Guid>> users = _usersService.GetUsers();

            if (!users.Any())
            {
                return NotFound("No users were found");
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("User Id cannot be empty.");
            }
            IdentityUser<Guid> user = await _usersService.GetUserById(userId);

            if (user == null)
            {
                return NotFound("No user was found");
            }

            return Ok(user);
        }

        //[HttpGet("email")]
        //public async Task<IActionResult> GetUserByEmail(string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //    {
        //        return BadRequest("email cannot be empty.");
        //    }
        //    IdentityUser<Guid> user = await _usersService.GetUserByEmail(email);

        //    if (user == null)
        //    {
        //        return NotFound("No user was found");
        //    }

        //    return Ok(user);
        //}

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null)
            {
                return BadRequest("Login request cannot be empty");
            }
            AuthResponseDTO authResponse = await _usersService.SignInAsync(loginRequestDTO);

            if (authResponse == null)
            {
                return StatusCode(500, "Error while logging in.");
            }

            if (!authResponse.Success)
            {
                return BadRequest("Error while logging in.");
            }
            HttpContext.Response.Cookies.Append("my_jwt", authResponse.Token);
            return Ok(authResponse);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequestDTO)
        {
            if (registerRequestDTO == null)
            {
                return BadRequest("Register request cannot be empty");
            }
            AuthResponseDTO authResponse = await _usersService.RegisterAsync(registerRequestDTO);

            if (authResponse == null)
            {
                return StatusCode(500, "Error while registering.");
            }

            if (!authResponse.Success)
            {
                return BadRequest("Error while registering.");
            }


            HttpContext.Response.Cookies.Append("my_jwt", authResponse.Token);
            return Ok(authResponse);
        }


        [HttpPost("logout")]
        public async Task<IActionResult> SignOut()
        {
            if (User.Identity!=null && User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return Ok(new { message = "Logged out." });
            }
            return NoContent();
        }

        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);

            return Ok(new { userId, roles });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(string? expiredToken)
        {
            try
            {
                return Ok(new { token = await _jwtService.RefreshJwtTokenAsync(expiredToken) });

            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
