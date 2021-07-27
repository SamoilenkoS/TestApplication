using DataAccessLayer.Models;

namespace BussinessLayer.JWT.Services
{
    public interface IAuthService
    {
        ValidationResult Login(AuthenticationModel authenticationModel);
        bool RegisterUser(User userToRegister);
    }
}
