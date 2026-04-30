using WeatherWebService.Models;

namespace WeatherWebService.Services;

public interface IWeatherService
{
    /// <summary>
    /// Retrieves a weather forecast for the requested city.
    /// </summary>
    /// <param name="city">The city query to send to OpenWeatherMap.</param>
    /// <param name="days">The number of days to return.</param>
    /// <returns>A normalized weather forecast response.</returns>
    Task<WeatherResponse> GetWeatherAsync(string city, int days);
}
