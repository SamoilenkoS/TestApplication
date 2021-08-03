using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly EFCoreContext _dbContext;

        public UserRolesRepository(EFCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddUserRole(AddUserRoleModel addUserRoleModel)
        {
            _dbContext.UserRoles.Add(new UserRoles
            {
                UserId = addUserRoleModel.UserId,
                RoleId = _dbContext.Roles.First(x => x.Role == addUserRoleModel.RoleTitle).Id
            });

            return _dbContext.SaveChanges() != 0;
        }
    }
}
