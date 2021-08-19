using System;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessLayer.Helpers.Interfaces;

namespace WebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;

        public UsersController(IAuthService authService, IUserService userService, ISessionService sessionService)
        {
            _authService = authService;
            _userService = userService;
            _sessionService = sessionService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticationModel authenticationModel)
        {
            var validationResult = await _authService.Login(authenticationModel);

            if (validationResult.IsSuccessful == false)
            {
                return BadRequest("Uncorrect login or password");
            }

            var token = _sessionService.CreateAuthToken(validationResult.UserWithRoles);

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(User userToRegister)
        {
            var leadGuid = await _authService.RegisterUser(userToRegister, $"{Request.Scheme}://{Request.Host}");

            if (leadGuid != Guid.Empty)
            {
                return Ok(userToRegister.Login);
            }

            return BadRequest("Invalid registration data");
        }

        [AllowAnonymous]
        [HttpGet("confirm")]
        public async Task<IActionResult> Confirm(string message)
        {
            var result = _authService.ConfirmEmail(message);
            if (result.IsSuccessful)
            {
                await _userService.AddUserRole(
                    new AddUserRoleModel
                    {
                        UserId = result.UserId.Value,
                        RoleTitle = "Administrator"
                    });
            }

            return Ok(result.IsSuccessful);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task TestPost(string firstName, string lastName)
        {
            var t = firstName + lastName;
        }
    }
}
