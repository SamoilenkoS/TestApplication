using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRolesRepository
    {
        bool AddUserRole(AddUserRoleModel addUserRoleModel);
    }
}
