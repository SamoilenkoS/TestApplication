using System;
using DataAccessLayer.Models;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace BusinessLayer.Helpers.Interfaces
{
    public interface IAuthService
    {
        Task<ValidationResult> Login(AuthenticationModel authenticationModel);
        Task<Guid> RegisterUser(User userToRegister, string path);
        ConfirmationResult ConfirmEmail(string message);
    }
}
