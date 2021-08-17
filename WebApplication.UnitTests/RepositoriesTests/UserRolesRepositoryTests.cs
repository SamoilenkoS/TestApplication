using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Helpers;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NUnit.Framework;

namespace WebApplication.UnitTests.RepositoriesTests
{
    public class UserRolesRepositoryTests
    {
        private readonly EFCoreContext _dbContext;
        private readonly Mock<IDistributedCache> _cacheMock;
        private readonly IUserRolesRepository _userRolesRepository;

        public UserRolesRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EFCoreContext>()
                .UseInMemoryDatabase(databaseName: "MovieListDatabase").Options;
            _cacheMock = new Mock<IDistributedCache>();
            _dbContext = new EFCoreContext(options);

            _userRolesRepository = new UserRolesRepository(_dbContext, _cacheMock.Object);
        }

        [SetUp]
        public void SetupBeforeEachTest()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Test]
        public async Task GetUserRolesByIdAsync_WhenUserHasRole_ShouldReturnRole()
        {
            var userId = Guid.NewGuid();
            var user = new UserDTO
            {
                Id = userId,
                BirthDate = DateTime.Now,
                FirstName = StringGenerator.GenerateString(),
                LastName = StringGenerator.GenerateString(),
                Login = StringGenerator.GenerateString(),
                Password = StringGenerator.GenerateString()
            };
            var testRole = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Role = "Admin"
            };

            _cacheMock.Setup(x
                => x.GetAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>())).ReturnsAsync((byte[])null);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.Roles.AddAsync(testRole);
            await _dbContext.UserRoles.AddAsync(new UserRoles
            {
                RoleId = testRole.Id,
                UserId = user.Id
            });
            await _dbContext.SaveChangesAsync();

            var temp = await _dbContext.Users.ToListAsync();

            var actualResult = await _userRolesRepository.GetUserRolesByIdAsync(userId);

            actualResult.Should().ContainEquivalentOf(testRole.Role);
        }

        [Test]
        public async Task GetUserRolesByIdAsync_WhenUserHasNoRole_ShouldReturnEmptyResult()
        {
            var userId = Guid.NewGuid();
            var user = new UserDTO
            {
                Id = userId,
                BirthDate = DateTime.Now,
                FirstName = StringGenerator.GenerateString(),
                LastName = StringGenerator.GenerateString(),
                Login = StringGenerator.GenerateString(),
                Password = StringGenerator.GenerateString()
            };
            var testRole = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Role = "Admin"
            };

            _cacheMock.Setup(x
                => x.GetAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>())).ReturnsAsync((byte[])null);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.Roles.AddAsync(testRole);
            await _dbContext.SaveChangesAsync();

            var temp = await _dbContext.Users.ToListAsync();

            var actualResult = await _userRolesRepository.GetUserRolesByIdAsync(userId);

            actualResult.Count().Should().Be(0);
        }
    }
}
