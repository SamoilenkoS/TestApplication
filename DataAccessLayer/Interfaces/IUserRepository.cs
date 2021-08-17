using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        UserDTO GetUserByAuthData(AuthenticationModel authenticationModel);
        Task RegisterUser(UserDTO userToRegister);
    }
}
