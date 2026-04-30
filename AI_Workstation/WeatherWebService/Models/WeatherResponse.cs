namespace WeatherWebService.Models;

public sealed class WeatherResponse
{
    public string City { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public List<DayForecast> Forecasts { get; set; } = [];
}
