using System.Text.Json.Serialization;

namespace WeatherWebService.Models;

internal sealed record OpenWeatherMain
{
    [JsonPropertyName("temp")]
    public double Temperature { get; init; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; init; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; init; }
}
