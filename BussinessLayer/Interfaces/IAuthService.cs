using BussinessLayer.Models;
using DataAccessLayer.Models;
using System.Threading.Tasks;

namespace BussinessLayer.JWT.Services
{
    public interface IAuthService
    {
        Task<ValidationResult> Login(AuthenticationModel authenticationModel);
        bool RegisterUser(User userToRegister, string path);
        ConfirmationResult ConfirmEmail(string message);
    }
}
