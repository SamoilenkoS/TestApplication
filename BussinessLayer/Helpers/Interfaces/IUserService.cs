using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace BusinessLayer.Helpers.Interfaces
{
    public interface IUserService
    {
        void AddUserMail(Guid userId, string mail, string path);
        Task<IEnumerable<string>> GetUserRolesById(Guid userId);
        User GetUserByLoginAndPassword(AuthenticationModel authenticationModel);
        Task<Guid> RegisterUser(UserDTO userToRegister);
        ConfirmationResult ConfirmEmail(string message);
        Task<bool> AddUserRole(AddUserRoleModel addUserRoleModel);
    }
}
