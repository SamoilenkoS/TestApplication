using BussinessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BussinessLayer.JWT.Services
{
    public class AuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public ValidationResult Login(AuthenticationModel authenticationModel)
        {
            var user = _userService.GetUserByLoginAndPassword(authenticationModel);

            if (user != null)
            {
                var roles = _userService.GetUserRolesById(user.Id);
                return new ValidationResult(true,
                    new UserWithRoles
                    {
                        UserId = user.Id,
                        Roles = roles
                    });
            }

            return new ValidationResult(false, null);
        }
    }
}