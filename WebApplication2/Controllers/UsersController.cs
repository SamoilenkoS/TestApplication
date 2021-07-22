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
        private readonly AuthService _authService;
        private readonly ISessionService _sessionService;

        public UsersController(AuthService authService, ISessionService sessionService)
        {
            _authService = authService;
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
        public string Register(User userToRegister)
        {
            var registered = _authService.Register(userToRegister);
            
            if (registered)
            {
                return Login(new AuthenticationModel
                {
                    Login = userToRegister.Login,
                    Password = userToRegister.Password
                });
            }

            BadRequest("Invalid registration data");
            return string.Empty;
        }
    }
}
