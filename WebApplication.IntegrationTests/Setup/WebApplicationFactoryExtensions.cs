using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WebApplication;

namespace Registrations.Api.IntegrationTests.Infrastructure
{
    public static class WebApplicationFactoryExtensions
    {

        public static T GetService<T>(this WebApplicationFactory<Startup> factory)
        {
            return factory.Services.GetRequiredService<T>();
        }
    }
}