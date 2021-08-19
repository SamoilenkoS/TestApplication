using AutoFixture;
using DataAccessLayer.Models;
using Newtonsoft.Json;
using Registrations.Api.IntegrationTests.Infrastructure;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using FluentAssertions;
using Xunit;

namespace WebApplication.IntegrationTests
{
    [Collection(nameof(RegistrationTestsCollection))]
    public class RegistrationTests : IAsyncLifetime
    {
        private readonly Fixture _fixture;
        private readonly HttpClient _client;
        private readonly EFCoreContext _dbContext;

        public RegistrationTests(RegistrationTestsFixture fixture)
        {
            _fixture = fixture.Fixture;
            _dbContext = fixture.Database;
            _client = fixture.Client;
        }

        public Task DisposeAsync()
        {
            _dbContext.Database.EnsureDeleted();
            return Task.CompletedTask;
        }

        public Task InitializeAsync()
        {
            _dbContext.Database.EnsureCreated();
            return Task.CompletedTask;
        }

        [Fact]
        public async Task ShouldRegisterLead_WhenInputIsCorrect()
        {
            var user = _fixture.Build<User>()
                .With(x => x.Password, "StrongPa$word!")
                .Without(x => x.Email)
                .Create();
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            await _client.PostAsync("users/register", data);

            _dbContext.Users.Should().ContainEquivalentOf(
                new
                {
                    user.FirstName,
                    user.LastName,
                    user.Login
                }
            );
        }
    }
}