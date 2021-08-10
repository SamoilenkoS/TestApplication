using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<bool> AddUserRole(AddUserRoleModel addUserRoleModel);
        Task<IEnumerable<string>> GetUserRolesById(Guid userId);
    }
}
