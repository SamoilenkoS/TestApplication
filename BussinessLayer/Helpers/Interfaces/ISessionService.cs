using DataAccessLayer.Models;

namespace BusinessLayer.Helpers.Interfaces
{
    public interface ISessionService
    {
        string CreateAuthToken(UserWithRoles user);
    }
}
