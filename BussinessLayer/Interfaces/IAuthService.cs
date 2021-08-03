using BussinessLayer.Models;
using DataAccessLayer.Models;

namespace BussinessLayer.JWT.Services
{
    public interface IAuthService
    {
        ValidationResult Login(AuthenticationModel authenticationModel);
        bool RegisterUser(User userToRegister, string path);
        ConfirmationResult ConfirmEmail(string message);
    }
}
