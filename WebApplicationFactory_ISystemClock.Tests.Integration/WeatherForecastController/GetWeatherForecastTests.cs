using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace WebApplicationFactory_ISystemClock.Tests.Integration.WeatherForecastController;

[Collection(ApiCollection.Definition)]
public sealed class GetWeatherForecastTests
{
  private readonly ApiFactory _factory;

  public GetWeatherForecastTests(ApiFactory factory)
  {
    _factory = factory;
  }

  [Fact]
  public async Task Get_WhenTodayIsWed_ShouldReturn11Records()
  {
    // Assert
    var httpClient = _factory.CreateClient();
    var expectedRecords = 11;

    var timeKey = Guid.NewGuid().ToString();
    var time = new DateTimeOffset(2023, 3, 1, 9, 0, 0, TimeSpan.Zero);
    ApiFactory.TestTimes.AddOrUpdate(timeKey, time, (k, v) => time);
    httpClient.DefaultRequestHeaders.Add("X-Test-Time", timeKey);

    // Act
    var response = await httpClient.GetAsync("/WeatherForecast");

    // Assert
    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    var data = await response.Content.ReadFromJsonAsync<IReadOnlyList<WeatherForecast>>();
    data.Should().NotBeNull();
    data!.Count.Should().Be(expectedRecords);
  }
}
