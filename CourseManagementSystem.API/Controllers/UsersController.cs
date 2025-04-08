﻿using CourseManagementSystem.Core.DTOs.User;
using CourseManagementSystem.Core.ServiceContracts;
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

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
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

            return Ok(authResponse);
        }


        [HttpPost("logout")]
        public async Task<IActionResult> SignOut()
        {
            bool signOutSuccess = await _usersService.SignOutAsync();

            if (!signOutSuccess)
            {
                return StatusCode(500, "Error while logging out.");
            }
            return Ok(signOutSuccess);
        }
    }
}
