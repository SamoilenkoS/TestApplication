using BussinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace BussinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserByLoginAndPassword(AuthenticationModel authenticationModel)
        {
            return _userRepository.GetUserByAuthData(authenticationModel);
        }

        public IEnumerable<string> GetUserRolesById(Guid userId)
        {
            return _userRepository.GetUserRolesById(userId);
        }

        public bool RegisterUser(User userToRegister)
        {
            try
            {
                userToRegister.Id = Guid.NewGuid();
                _userRepository.RegisterUser(userToRegister);

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
