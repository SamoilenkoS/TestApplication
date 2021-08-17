using AutoMapper;
using AutoMapper.Configuration;
using DataAccessLayer.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using BusinessLayer.Profiles;

namespace WebApplication.UnitTests.MapperTests
{
    public class UsersMappingTests
    {
        private readonly IMapper _mapper;
        public UsersMappingTests()
        {
            _mapper = GetMapper();
        }

        [Test]
        public void User_To_UserDTO_Mapping()
        {
            var user = new User
            {
                BirthDate = DateTime.Now,
                Email = "Test",
                FirstName = "Test2",
                LastName = "Test3",
                Id = Guid.NewGuid(),
                Login = "Test4",
                Password = "Test5"
            };

            var userDTO = _mapper.Map<UserDTO>(user);

            userDTO.Should().BeEquivalentTo(new
            {
                user.Id,
                user.BirthDate,
                user.FirstName,
                user.Login,
                user.LastName,
                user.Password
            });
        }

        [Test]
        public void UserDTO_To_User_Mapping()
        {
            var userDTO = new UserDTO
            {
                BirthDate = DateTime.Now,
                FirstName = "Test2",
                LastName = "Test3",
                Id = Guid.NewGuid(),
                Login = "Test4",
                Password = "Test5"
            };

            var user = _mapper.Map<User>(userDTO);

            user.Should().BeEquivalentTo(new
            {
                userDTO.Id,
                userDTO.BirthDate,
                userDTO.FirstName,
                userDTO.Login,
                userDTO.LastName
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
