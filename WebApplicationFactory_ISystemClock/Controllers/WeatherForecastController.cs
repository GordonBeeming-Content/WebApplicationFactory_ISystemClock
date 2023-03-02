namespace WebApplicationFactory_ISystemClock.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  private static readonly string[] _summaries = new[]
  {
      "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  };

  private readonly ILogger<WeatherForecastController> _logger;
  private readonly ISystemClock _systemClock;

  public WeatherForecastController(ILogger<WeatherForecastController> logger, ISystemClock systemClock)
  {
    _logger = logger;
    _systemClock = systemClock;
  }

  [HttpGet(Name = "GetWeatherForecast")]
  public IEnumerable<WeatherForecast> Get()
  {
    var daysToNextSunday = GetDaysUntilNextSunday();
    return Enumerable.Range(1, daysToNextSunday).Select(index => new WeatherForecast
    {
      Date = DateOnly.FromDateTime(_systemClock.UtcNow.AddDays(index).UtcDateTime),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = _summaries[Random.Shared.Next(_summaries.Length)]
    })
    .ToArray();
  }

  private int GetDaysUntilNextSunday()
  {
    var daysToNextSunday = 0;
    var pastASunday = false;
    while (true)
    {
      daysToNextSunday++;
      if (_systemClock.UtcNow.AddDays(daysToNextSunday).DayOfWeek == DayOfWeek.Sunday)
      {
        if (pastASunday)
        {
          break;
        }
        pastASunday = true;
      }
    }

    return daysToNextSunday;
  }
}
