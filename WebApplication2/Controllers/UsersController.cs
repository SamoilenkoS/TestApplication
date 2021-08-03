using BussinessLayer.Interfaces;
using BussinessLayer.JWT;
using BussinessLayer.JWT.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
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
        public string Login(AuthenticationModel authenticationModel)
        {
            var validationResult = _authService.Login(authenticationModel);

            if (validationResult.IsSuccessful == false)
            {
                BadRequest("Uncorrect login or password");
                return string.Empty;
            }

            var token = _sessionService.CreateAuthToken(validationResult.UserWithRoles);

            return token;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(User userToRegister)
        {
            var registered = _authService.RegisterUser(userToRegister, $"{Request.Scheme}://{Request.Host}");

            if (registered)
            {
                return Ok(Login(new AuthenticationModel
                {
                    Login = userToRegister.Login,
                    Password = userToRegister.Password
                }));
            }

            return BadRequest("Invalid registration data");
        }

        [AllowAnonymous]
        [HttpGet("confirm")]
        public IActionResult Confirm(string message)
        {
            var result = _authService.ConfirmEmail(message);
            if (result.IsSuccessful)
            {
                _userService.AddUserRole(
               new AddUserRoleModel
               {
                   UserId = result.UserId.Value,
                   RoleTitle = "Administrator"
               });
            }

            return Ok(result.IsSuccessful);
        }
    }
}
