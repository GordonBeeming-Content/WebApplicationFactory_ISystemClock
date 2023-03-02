namespace WebApplicationFactory_ISystemClock.Tests.Integration;

public sealed class ApiFactory : WebApplicationFactory<IApiMarker>
{
  internal static ConcurrentDictionary<string, DateTimeOffset> TestTimes { get; } = new();

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureTestServices(services =>
    {
      services.AddHttpContextAccessor();
      var systemClockMock = new Mock<ISystemClock>();
      services.AddSingleton(sp =>
          {
            systemClockMock.SetupGet(mock => mock.UtcNow)
                .Returns(() =>
                {
                  var httpContext = sp.GetRequiredService<IHttpContextAccessor>();
                  if (httpContext.HttpContext?.Request.Headers.ContainsKey("X-Test-Time") == true)
                  {
                    var timeKey = httpContext.HttpContext?.Request.Headers["X-Test-Time"][0]!;
                    if (TestTimes.ContainsKey(timeKey))
                    {
                      var time = TestTimes[timeKey];
                      var newTime = time.AddMilliseconds(1);
                      TestTimes.AddOrUpdate(timeKey, newTime, (k, v) => newTime);
                      return time;
                    }
                  }
                  return DateTimeOffset.UtcNow;
                });
            return systemClockMock.Object;
          });
    });
  }
}
