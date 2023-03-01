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

    // Act
    var response = await httpClient.GetAsync("/WeatherForecast");

    // Assert
    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    var data = await response.Content.ReadFromJsonAsync<IReadOnlyList<WeatherForecast>>();
    data.Should().NotBeNull();
    data!.Count.Should().Be(expectedRecords);
  }
}
