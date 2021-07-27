using DataAccessLayer.Models;

namespace BussinessLayer.JWT
{
    public interface ISessionService
    {
        string CreateAuthToken(UserWithRoles user);
    }
}
