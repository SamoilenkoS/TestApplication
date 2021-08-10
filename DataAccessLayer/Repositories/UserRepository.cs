using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EFCoreContext _dbContext;

        public UserRepository(EFCoreContext dbContext)
        {
            _dbContext = dbContext;
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

            return true;
        }
    }
}
