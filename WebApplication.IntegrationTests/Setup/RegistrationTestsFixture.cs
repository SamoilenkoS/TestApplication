using System;
using System.Linq;
using System.Net.Http;
using AutoFixture;
using DataAccessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication;
using Xunit;

namespace Registrations.Api.IntegrationTests.Infrastructure
{
    [Collection(nameof(RegistrationTestsCollection))]
    public class RegistrationTestsFixture : IDisposable
    {
        public EFCoreContext Database { get; }
        public HttpClient Client { get; }
        public Fixture Fixture { get; }

        private readonly WebApplicationFactory<Startup> _factory;

        public RegistrationTestsFixture()
        {
            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(DbContextOptions<EFCoreContext>));
                    services.Remove(descriptor);

                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkSqlServer()
                        .BuildServiceProvider();

                    services.AddDbContext<EFCoreContext>(options
                        => {
                        options.UseSqlServer(
                            $"Server=(localdb)\\mssqllocaldb;Database=test_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                            .UseInternalServiceProvider(serviceProvider);
                    }, ServiceLifetime.Singleton);
                    services.BuildServiceProvider();
                }).UseDefaultServiceProvider(options => options.ValidateScopes = false);
            });

            var clientOptions = new WebApplicationFactoryClientOptions();
            Client = _factory.CreateClient(clientOptions);
            Database = _factory.GetService<EFCoreContext>();

            Fixture = new Fixture();
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}