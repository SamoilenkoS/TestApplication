using DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<string> GetUserRolesById(Guid userId);
        User GetUserByAuthData(AuthenticationModel authenticationModel);
        bool RegisterUser(User userToRegister);
    }
}
