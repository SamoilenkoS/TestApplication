using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using System.Collections.Generic;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApplication.DataAccessLayer;
using Microsoft.Extensions.Caching.Distributed;

namespace DataAccessLayer.Repositories
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly IDistributedCache _cache;
        private readonly EFCoreContext _dbContext;

        public UserRolesRepository(EFCoreContext dbContext, IDistributedCache cache)
        {
            _cache = cache;
            _dbContext = dbContext;
        }

        public async Task<bool> AddUserRole(AddUserRoleModel addUserRoleModel)
        {
            await _dbContext.UserRoles.AddAsync(new UserRoles
            {
                UserId = addUserRoleModel.UserId,
                RoleId = (await _dbContext.Roles.FirstAsync(x => x.Role == addUserRoleModel.RoleTitle)).Id
            });

            var isAdded = await _dbContext.SaveChangesAsync() != 0;

            if(isAdded)
            {
                await AddUserRoleToCache(addUserRoleModel);
            }

            return isAdded;
        }

        public async Task<IEnumerable<string>> GetUserRolesByIdAsync(Guid userId)
        {
            var usersWithRoles = await GetAllUserRolesFromCache();

            return usersWithRoles.FirstOrDefault(x => x.UserId == userId)?.Roles ?? new List<string>();
        }

        private async Task AddUserRoleToCache(AddUserRoleModel addUserRoleModel)
        {
            var usersWithRoles = await GetAllUserRolesFromCache();
            var user = usersWithRoles.FirstOrDefault(x => x.UserId == addUserRoleModel.UserId);
            var userExists = user != null;
            if(!userExists)
            {
                user = new UserWithRoles
                {
                    UserId = addUserRoleModel.UserId,
                    Roles = new List<string>()
                };
            }

            user.Roles.Add(addUserRoleModel.RoleTitle);
            if(!userExists)
            {
                usersWithRoles.Add(user);
            }

            await _cache.SetRecordAsync(nameof(UserWithRoles), usersWithRoles);
        }

        private async Task<IList<UserWithRoles>> GetAllUserRolesFromCache()
        {
            var userWithRoles = await _cache.GetRecordAsync<IList<UserWithRoles>>(nameof(UserWithRoles));
            if (userWithRoles == null)
            {
                userWithRoles = await GetAllUserRolesFromDb();
                await _cache.SetRecordAsync(nameof(UserWithRoles), userWithRoles);
            }

            return userWithRoles;
        }

        private async Task<IList<UserWithRoles>> GetAllUserRolesFromDb()
        {
            var items = await (from user in _dbContext.Set<UserDTO>()
                         join userRole in _dbContext.Set<UserRoles>()
                            on user.Id equals userRole.UserId
                         join role in _dbContext.Set<RoleEntity>()
                            on userRole.RoleId equals role.Id
                         select new
                         {
                             role.Role,
                             UserId = user.Id
                         }).ToListAsync();

            return items.GroupBy(x => x.UserId)
                .Select(x => new UserWithRoles
                {
                    UserId = x.Key,
                    Roles = x.ToList().Select(x => x.Role).ToList()
                }).ToList();
        }
    }
}