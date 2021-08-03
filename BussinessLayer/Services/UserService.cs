using AutoMapper;
using BussinessLayer.Helpers;
using BussinessLayer.Interfaces;
using BussinessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BussinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IMailService _mailService;
        private readonly IMailExchangerService _mailExchangerService;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IUserRolesRepository userRolesRepository,
            IMailService mailService,
            IMailExchangerService mailExchangerService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userRolesRepository = userRolesRepository;
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

        public void AddUserMail(Guid userId, string mail, string path)
        {
            var confirmationModel = new ConfirmationMessageModel
            {
                ConfirmationMessage = StringGenerator.GenerateString(),
                UserId = userId
            };
            var modelToSerialize = JsonSerializer.Serialize(confirmationModel);
            var messageToSend = EncryptionHelper.Encrypt(modelToSerialize);

            _mailService.SaveMailAddress(new EmailDTO
            {
                ConfirmationMessage = confirmationModel.ConfirmationMessage,
                Email = mail,
                IsConfirmed = false,
                UserId = userId
            });
            var confirmationString = $"{path}/users/confirm?message={messageToSend}";

            _mailExchangerService.SendMessage(
                mail,
                "Email confirmation",
                confirmationString);

            WriteStringToFile(confirmationString);
        }

        public ConfirmationResult ConfirmEmail(string message)
        {
            try
            {
                var decrypted = EncryptionHelper.Decrypt(message);
                var model = JsonSerializer.Deserialize<ConfirmationMessageModel>(decrypted);
                var isSuccessful = _mailService.ConfirmMail(model);
                return new ConfirmationResult
                {
                    IsSuccessful = isSuccessful,
                    UserId = model.UserId
                };
            }
            catch(Exception ex)
            {
                return new ConfirmationResult { IsSuccessful = false };
            }

        }

        public bool AddUserRole(AddUserRoleModel addUserRoleModel)
        {
            var result = _userRolesRepository.AddUserRole(addUserRoleModel);
            _userRepository.AddUserRole(addUserRoleModel);

            return result;
        }

        private void WriteStringToFile(string message)
        {
            using(var streamWriter = new StreamWriter("confirmationString.txt"))
            {
                streamWriter.WriteLine(message);
            }
        }
    }
}
