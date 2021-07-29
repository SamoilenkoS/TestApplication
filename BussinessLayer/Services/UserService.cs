using AutoMapper;
using BussinessLayer.Helpers;
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
        private readonly IMailService _mailService;
        private readonly IMailExchangerService _mailExchangerService;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IMailService mailService,
            IMailExchangerService mailExchangerService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mailService = mailService;
            _mailExchangerService = mailExchangerService;
            _mapper = mapper;
        }

        public bool RegisterUser(UserDTO userToRegister)
        {
            try
            {
                userToRegister.Id = Guid.NewGuid();
                _userRepository.RegisterUser(userToRegister);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User GetUserByLoginAndPassword(AuthenticationModel authenticationModel)
            => _mapper.Map<User>(_userRepository.GetUserByAuthData(authenticationModel));

        public IEnumerable<string> GetUserRolesById(Guid userId)
            => _userRepository.GetUserRolesById(userId);

        public void AddUserMail(Guid userId, string mail)
        {
            var confirmationMessage = StringGenerator.GenerateString();

            _mailService.SaveMailAddress(new EmailDTO
            {
                ConfirmationMessage = confirmationMessage,
                Email = mail,
                IsConfirmed = false,
                UserId = userId
            });

            _mailExchangerService.SendMessage(
                mail,
                "Email confirmation",
                confirmationMessage);
        }
    }
}
