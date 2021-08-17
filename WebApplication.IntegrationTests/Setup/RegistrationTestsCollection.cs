using Xunit;

namespace Registrations.Api.IntegrationTests.Infrastructure
{
  [CollectionDefinition(nameof(RegistrationTestsCollection))]
  public class RegistrationTestsCollection : ICollectionFixture<RegistrationTestsFixture>
  {
  }
}
