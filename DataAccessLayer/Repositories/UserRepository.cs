using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private class UserWithRoles
        {
            public Guid UserId { get; set; }
            public IEnumerable<string> Roles { get; set; }
        }

        private readonly EFCoreContext _dbContext;
        private IEnumerable<UserWithRoles> _usersWithRoles;

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

        public User GetUserByAuthData(AuthenticationModel authenticationModel)
        {
            return _dbContext.Users
                .FirstOrDefault(x =>
                x.Login == authenticationModel.Login &&
                x.Password == authenticationModel.Password);
        }

        private IEnumerable<UserWithRoles> GetAllUserRoles()
        {
            var items = (from user in _dbContext.Set<User>()
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
                    Roles = x.ToList().Select(x => x.Role)
                });
        }

        public bool RegisterUser(User userToRegister)
        {
            _dbContext.Users.Add(userToRegister);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
