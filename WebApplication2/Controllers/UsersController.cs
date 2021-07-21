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
            }

            var token = _sessionService.CreateAuthToken(validationResult.UserWithRoles);

            return token;
        }
    }
}
