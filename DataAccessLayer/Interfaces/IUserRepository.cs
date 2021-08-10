using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        UserDTO GetUserByAuthData(AuthenticationModel authenticationModel);
        bool RegisterUser(UserDTO userToRegister);
    }
}
