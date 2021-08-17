using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task RegisterUser(UserDTO userToRegister)
        {
            await _dbContext.Users.AddAsync(userToRegister);
            await _dbContext.SaveChangesAsync();
        }
    }
}
