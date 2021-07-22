using DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace BussinessLayer.Interfaces
{
    public interface IUserService
    {
        IEnumerable<string> GetUserRolesById(Guid userId);
        User GetUserByLoginAndPassword(AuthenticationModel authenticationModel);
        bool RegisterUser(User userToRegister);
    }
}
