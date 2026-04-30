using System.Text.Json.Serialization;

namespace WeatherWebService.Models;

internal sealed record OpenWeatherCondition
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("icon")]
    public string? Icon { get; init; }
}
