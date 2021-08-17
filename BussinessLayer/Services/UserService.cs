using AutoMapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLayer.Helpers;
using BusinessLayer.Helpers.Interfaces;
using BusinessLayer.Models;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly IUserRepository _userRepository;
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IMailService _mailService;
        private readonly IMailExchangerService _mailExchangerService;
        private readonly IMapper _mapper;

        public UserService(
            IEncryptionService encryptionService,
            IUserRepository userRepository,
            IUserRolesRepository userRolesRepository,
            IMailService mailService,
            IMailExchangerService mailExchangerService,
            IMapper mapper)
        {
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _userRolesRepository = userRolesRepository;
            _mailService = mailService;
            _mailExchangerService = mailExchangerService;
            _mapper = mapper;
        }

        public async Task<Guid> RegisterUser(UserDTO userToRegister)
        {
            try
            {
                userToRegister.Id = Guid.NewGuid();
                await _userRepository.RegisterUser(userToRegister);

                return userToRegister.Id;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

        public User GetUserByLoginAndPassword(AuthenticationModel authenticationModel)
            => _mapper.Map<User>(_userRepository.GetUserByAuthData(authenticationModel));

        public async Task<IEnumerable<string>> GetUserRolesById(Guid userId)
            => await _userRolesRepository.GetUserRolesByIdAsync(userId);

        public void AddUserMail(Guid userId, string mail, string path)
        {
            var confirmationModel = new ConfirmationMessageModel
            {
                ConfirmationMessage = StringGenerator.GenerateString(),
                UserId = userId
            };
            var modelToSerialize = JsonSerializer.Serialize(confirmationModel);
            var messageToSend = _encryptionService.Encrypt(modelToSerialize);

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
        }

        public ConfirmationResult ConfirmEmail(string message)
        {
            try
            {
                var decrypted = _encryptionService.Decrypt(message);
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

        public async Task<bool> AddUserRole(AddUserRoleModel addUserRoleModel)
            => await _userRolesRepository.AddUserRole(addUserRoleModel);
    }
}
