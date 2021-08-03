using DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<string> GetUserRolesById(Guid userId);
        UserDTO GetUserByAuthData(AuthenticationModel authenticationModel);
        bool RegisterUser(UserDTO userToRegister);
        void AddUserRole(AddUserRoleModel addUserRoleModel);
    }
}
