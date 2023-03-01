namespace WebApplicationFactory_ISystemClock.Tests.Integration;

public sealed class ApiFactory : WebApplicationFactory<IApiMarker>
{
  internal static ConcurrentDictionary<string, DateTimeOffset> TestTimes { get; } = new();

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureTestServices(services =>
    {
      
    });
  }
}
