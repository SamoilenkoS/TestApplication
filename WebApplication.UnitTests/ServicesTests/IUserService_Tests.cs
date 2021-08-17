using AutoMapper;
using AutoMapper.Configuration;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLayer.Helpers.Interfaces;
using BusinessLayer.Models;
using BusinessLayer.Profiles;
using BusinessLayer.Services;
using FluentAssertions;

namespace WebApplication.UnitTests
{
    public class IUserService_Tests
    {
        private readonly IUserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserRolesRepository> _userRolesRepositoryMock;
        private readonly Mock<IMailService> _mailServiceMock;
        private readonly Mock<IMailExchangerService> _mailExchangerServiceMock;
        private readonly Mock<IEncryptionService> _encryptionServiceMock;

        public IUserService_Tests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userRolesRepositoryMock = new Mock<IUserRolesRepository>();
            _mailServiceMock = new Mock<IMailService>();
            _mailExchangerServiceMock = new Mock<IMailExchangerService>();
            _encryptionServiceMock = new Mock<IEncryptionService>();

            var mapper = GetMapper();

            _userService = new UserService(
                _encryptionServiceMock.Object,
                _userRepositoryMock.Object,
                _userRolesRepositoryMock.Object,
                _mailServiceMock.Object,
                _mailExchangerServiceMock.Object,
                mapper);
        }

        [Test]
        public void AddUserMail_WhenCalled_ShouldSaveMailAndSendConfirmationEmail()
        {
            var mail = "testmail@gmail.com";
            _mailServiceMock.Setup(x
                => x.SaveMailAddress(It.IsAny<EmailDTO>()))
                .Verifiable();

            _mailExchangerServiceMock.Setup(x 
                => x.SendMessage(mail, "Email confirmation", It.IsAny<string>()))
                .Verifiable();

            _userService.AddUserMail(Guid.NewGuid(), mail, "localhost");

            _mailServiceMock.Verify();
            _mailExchangerServiceMock.Verify();
        }

        [Test]
        public async Task GetUserRolesById_WhenCalled_ShouldReturnUsersRoles()
        {
            var userId = Guid.NewGuid();
            var expected = new List<string> {"Admin"};
            _userRolesRepositoryMock.Setup(x => x.GetUserRolesByIdAsync(userId))
                .ReturnsAsync(expected);

            var actual = await _userService.GetUserRolesById(userId);

            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public async Task RegisterUser_WhenCalled_Should()
        {
            var userDTO = new UserDTO
            {
                BirthDate = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 18)),
                FirstName = "Test",
                LastName = "Best",
                Id = Guid.NewGuid(),
                Login = "Login",
                Password = "StrongPa$s!"
            };

            var actual = await _userService.RegisterUser(userDTO);

            actual.Should().NotBe(Guid.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ConfirmMail_WhenCalled_Should(bool confirmationResult)
        {
            var message = "Test message";
            var messageModel = new ConfirmationMessageModel
            {
                ConfirmationMessage = message,
                UserId = Guid.NewGuid()
            };

            _encryptionServiceMock.Setup(x => x.Decrypt(message))
                .Returns(JsonSerializer.Serialize(messageModel));
            _mailServiceMock.Setup(x => x.ConfirmMail(It.Is<ConfirmationMessageModel>
                    (x => x.ConfirmationMessage == message && x.UserId == messageModel.UserId)))
                .Returns(confirmationResult);

            var actualResult = _userService.ConfirmEmail(message);

            actualResult.Should().BeEquivalentTo(new
            {
                IsSuccessful = confirmationResult,
                messageModel.UserId
            });
        }

        private static Mapper GetMapper()
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression();

            mapperConfigurationExpression.AddMaps(typeof(WeatherForecastProfile).Assembly);

            var mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression);
            mapperConfiguration.AssertConfigurationIsValid();

            var mapper = new Mapper(mapperConfiguration);
            return mapper;
        }
    }
}