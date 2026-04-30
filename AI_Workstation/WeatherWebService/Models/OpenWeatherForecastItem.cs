using System.Text.Json.Serialization;

namespace WeatherWebService.Models;

internal sealed record OpenWeatherForecastItem
{
    [JsonPropertyName("dt")]
    public long UnixTimestamp { get; init; }

    [JsonPropertyName("main")]
    public OpenWeatherMain? Main { get; init; }

    [JsonPropertyName("weather")]
    public List<OpenWeatherCondition> Weather { get; init; } = [];

    [JsonPropertyName("wind")]
    public OpenWeatherWind? Wind { get; init; }
}
