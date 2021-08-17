using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Helpers;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NUnit.Framework;

namespace WebApplication.UnitTests.RepositoriesTests
{
    public class UserRolesRepositoryTests
    {
        private readonly Mock<EFCoreContext> _dbContextMock;
        private readonly Mock<IDistributedCache> _cacheMock;
        private readonly IUserRolesRepository _userRolesRepository;

        public UserRolesRepositoryTests()
        {
            _dbContextMock = new Mock<EFCoreContext>();
            _cacheMock = new Mock<IDistributedCache>();

            _userRolesRepository = new UserRolesRepository(_dbContextMock.Object, _cacheMock.Object);
        }

        [Test]
        public async Task Test()
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

            _dbContextMock.Setup(x => x.Set<UserDTO>())
                .Returns(DbContextMock.GetQueryableMockDbSet(new List<UserDTO>
                {
                    user
                }));
            _dbContextMock.Setup(x => x.Set<RoleEntity>())
                .Returns(DbContextMock.GetQueryableMockDbSet(new List<RoleEntity>
                {
                    testRole
                }));
            _dbContextMock.Setup(x => x.Set<UserRoles>())
                .Returns(DbContextMock.GetQueryableMockDbSet(new List<UserRoles>
                {
                    new UserRoles
                    {
                        RoleId = testRole.Id,
                        UserId = user.Id
                    }
                }));

            var actualResult = await _userRolesRepository.GetUserRolesByIdAsync(userId);

            actualResult.Should().BeEquivalentTo(new
            {
                testRole.Role
            });
        }
    }
}
