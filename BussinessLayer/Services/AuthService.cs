﻿using AutoMapper;
using BussinessLayer.Interfaces;
using BussinessLayer.Models;
using DataAccessLayer.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinessLayer.JWT.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;

        public AuthService(
            IUserService userService,
            IHashService hashService,
            IMapper mapper)
        {
            _userService = userService;
            _hashService = hashService;
            _mapper = mapper;
        }

        public ConfirmationResult ConfirmEmail(string message)
        {
            return _userService.ConfirmEmail(message);
        }

        public async Task<ValidationResult> Login(AuthenticationModel authenticationModel)
        {
            var hashedPassword = _hashService.HashString(authenticationModel.Password);
            authenticationModel.Password = hashedPassword;

            var user = _userService.GetUserByLoginAndPassword(authenticationModel);
            UserWithRoles userWithRoles = null;
            var isSuccessful = user != null;

            if (isSuccessful)
            {
                var roles = await _userService.GetUserRolesById(user.Id);
                userWithRoles = new UserWithRoles
                {
                    UserId = user.Id,
                    Roles = roles.ToList()
                };
            }

            return new ValidationResult
            {
                IsSuccessful = isSuccessful,
                UserWithRoles = userWithRoles
            };
        }

        public bool RegisterUser(User userToRegister, string path)
        {
            if (!IsPasswordValid(userToRegister.Password))
            {
                return false;
            }

            var userDTO = _mapper.Map<UserDTO>(userToRegister);
            userDTO.Password = _hashService.HashString(userToRegister.Password);

            var isRegistrationSuccessful = _userService.RegisterUser(userDTO);

            if (isRegistrationSuccessful &&
                !string.IsNullOrEmpty(userToRegister.Email))
            {
                _userService.AddUserMail(userDTO.Id, userToRegister.Email, path);
            }

            return isRegistrationSuccessful;
        }

        private bool IsPasswordValid(string password)
        {
            var regEx = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])((?=.*?[0-9])|(?=.*?[#?!@$%^&*-]))", RegexOptions.Compiled);

            return regEx.IsMatch(password);
        }
    }
}