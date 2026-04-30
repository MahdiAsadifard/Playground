using System.Text.Json.Serialization;

namespace WeatherWebService.Models;

internal sealed record OpenWeatherWind
{
    [JsonPropertyName("speed")]
    public double Speed { get; init; }
}
