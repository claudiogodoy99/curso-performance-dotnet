using Microsoft.AspNetCore.Mvc;

namespace demo_async_vs_sync.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly WeatherForecastRepository _repo;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        _repo = new();
    }

    [HttpGet("normal")]
    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        return _repo.Get();
    }

   [HttpGet("async")]
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
    {
        return await _repo.GetAsync();
    }
}
