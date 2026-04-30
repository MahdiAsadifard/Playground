using System.Text.Json.Serialization;

namespace WeatherWebService.Models;

internal sealed record OpenWeatherForecastResponse
{
    [JsonPropertyName("city")]
    public OpenWeatherCity? City { get; init; }

    [JsonPropertyName("list")]
    public List<OpenWeatherForecastItem>? List { get; init; }
}
