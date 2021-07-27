using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User GetUserByLoginAndPassword(AuthenticationModel authenticationModel)
            => _mapper.Map<User>(_userRepository.GetUserByAuthData(authenticationModel));

        public IEnumerable<string> GetUserRolesById(Guid userId)
            => _userRepository.GetUserRolesById(userId);

        public bool RegisterUser(UserDTO userToRegister)
        {
            try
            {
                userToRegister.Id = Guid.NewGuid();
                _userRepository.RegisterUser(userToRegister);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
