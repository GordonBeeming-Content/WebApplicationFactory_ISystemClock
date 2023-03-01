namespace WebApplicationFactory_ISystemClock.Tests.Integration;

[CollectionDefinition(Definition)]
public sealed class ApiCollection : ICollectionFixture<ApiFactory>
{
  public const string Definition = nameof(ApiCollection);
}
