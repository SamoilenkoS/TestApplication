using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static IList<UserWithRoles> _usersWithRoles;

        private readonly EFCoreContext _dbContext;

        public UserRepository(EFCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<string> GetUserRolesById(Guid userId)
        {
            if (_usersWithRoles == null)
            {
                _usersWithRoles = GetAllUserRoles();
            }

            return _usersWithRoles
                .FirstOrDefault(x => x.UserId == userId)?.Roles ?? new List<string>();
        }

        public UserDTO GetUserByAuthData(AuthenticationModel authenticationModel)
        {
            return _dbContext.Users
                .FirstOrDefault(x =>
                x.Login == authenticationModel.Login &&
                x.Password == authenticationModel.Password);
        }

        public bool RegisterUser(UserDTO userToRegister)
        {
            _dbContext.Users.Add(userToRegister);
            _dbContext.SaveChanges();

            AddUserWithEmptyRoles(userToRegister.Id);

            return true;
        }

        private IList<UserWithRoles> GetAllUserRoles()
        {
            var items = (from user in _dbContext.Set<UserDTO>()
                         join userRole in _dbContext.Set<UserRoles>()
                            on user.Id equals userRole.UserId
                         join role in _dbContext.Set<RoleEntity>()
                            on userRole.RoleId equals role.Id
                         select new
                         {
                             role.Role,
                             UserId = user.Id
                         }).ToList();

            return items.GroupBy(x => x.UserId)
                .Select(x => new UserWithRoles
                {
                    UserId = x.Key,
                    Roles = x.ToList().Select(x => x.Role).ToList()
                }).ToList();
        }

        private void AddUserWithEmptyRoles(Guid userId)
        {
            if (_usersWithRoles == null)
            {
                _usersWithRoles = GetAllUserRoles();
            }

            _usersWithRoles.Add(new UserWithRoles
            {
                Roles = new List<string>(),
                UserId = userId
            });
        }

        public void AddUserRole(AddUserRoleModel addUserRoleModel)
        {
            var user = _usersWithRoles.First(x => x.UserId == addUserRoleModel.UserId);
            user.Roles.Add(addUserRoleModel.RoleTitle);
        }
    }
}
