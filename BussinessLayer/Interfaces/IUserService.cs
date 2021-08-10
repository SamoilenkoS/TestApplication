using BussinessLayer.Models;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<string>> GetUserRolesById(Guid userId);
        User GetUserByLoginAndPassword(AuthenticationModel authenticationModel);
        bool RegisterUser(UserDTO userToRegister);
        void AddUserMail(Guid userId, string mail, string path);
        ConfirmationResult ConfirmEmail(string message);
        Task<bool> AddUserRole(AddUserRoleModel addUserRoleModel);
    }
}
