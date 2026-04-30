using System.Text.Json.Serialization;

namespace WeatherWebService.Models;

internal sealed record OpenWeatherCity
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("timezone")]
    public int Timezone { get; init; }
}
