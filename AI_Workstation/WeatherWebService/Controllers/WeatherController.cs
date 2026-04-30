using Microsoft.AspNetCore.Mvc;
using WeatherWebService.Models;
using WeatherWebService.Services;

namespace WeatherWebService.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
    {
        ArgumentNullException.ThrowIfNull(weatherService);
        ArgumentNullException.ThrowIfNull(logger);

        _weatherService = weatherService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves weather forecast data for the specified city.
    /// </summary>
    /// <param name="city">The city to query.</param>
    /// <param name="state">Optional state/province code (e.g., ON).</param>
    /// <param name="country">Optional country code (e.g., CA).</param>
    /// <param name="days">The number of forecast days to return.</param>
    /// <returns>The requested weather forecast.</returns>
    [HttpGet(Name = "GetWeather")]
    [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<WeatherResponse>> GetWeather(
        [FromQuery] string city = "Toronto",
        [FromQuery] string? state = null,
        [FromQuery] string? country = null,
        [FromQuery] int days = 5)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return BadRequest("City is required.");
        }

        if (days < 1)
        {
            return BadRequest("Days must be at least 1.");
        }

        try
        {
            var queryParts = new List<string> { city.Trim() };
            if (!string.IsNullOrWhiteSpace(state))
                queryParts.Add(state.Trim());
            if (!string.IsNullOrWhiteSpace(country))
                queryParts.Add(country.Trim());

            var scopedCity = string.Join(",", queryParts);
            var response = await _weatherService.GetWeatherAsync(scopedCity, days).ConfigureAwait(false);

            return Ok(response);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while retrieving weather for {City}.", city);
            return Problem(
                detail: "An unexpected error occurred while retrieving the weather forecast.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
