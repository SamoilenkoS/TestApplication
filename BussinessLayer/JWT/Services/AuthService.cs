using BussinessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BussinessLayer.JWT.Services
{
    public class AuthService
    {
        private readonly IUserService _userService;
        private readonly IHashService _hashService;

        public AuthService(IUserService userService, IHashService hashService)
        {
            _userService = userService;
            _hashService = hashService;
        }

        public ValidationResult Login(AuthenticationModel authenticationModel)
        {
            var hashedPassword = _hashService.HashString(authenticationModel.Password);
            authenticationModel.Password = hashedPassword;

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

        public bool Register(User userToRegister)
        {
            var originalPassword = userToRegister.Password;
            userToRegister.Password = _hashService.HashString(userToRegister.Password);

            var response = _userService.RegisterUser(userToRegister);

            userToRegister.Password = originalPassword;

            return response;
        }
    }
}