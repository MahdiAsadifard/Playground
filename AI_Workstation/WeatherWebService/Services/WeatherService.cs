using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherWebService.Models;

namespace WeatherWebService.Services;

public sealed class WeatherService : IWeatherService
{
    private static readonly TimeSpan Midday = TimeSpan.FromHours(12);

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(configuration);

        _httpClient = httpClient;
        _configuration = configuration;
    }

    /// <summary>
    /// Retrieves a weather forecast for the requested city.
    /// </summary>
    /// <param name="city">The city query to send to OpenWeatherMap.</param>
    /// <param name="days">The number of days to return.</param>
    /// <returns>A normalized weather forecast response.</returns>
    public async Task<WeatherResponse> GetWeatherAsync(string city, int days)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("A city name must be provided.", nameof(city));
        }

        if (days < 1 || days > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(days), "Days must be between 1 and 5.");
        }

        var apiKey = _configuration["OpenWeatherMap:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey) || string.Equals(apiKey, "YOUR_API_KEY_HERE", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("A valid OpenWeatherMap API key is required.");
        }

        using var response = await _httpClient.GetAsync(BuildRequestUri(city, apiKey)).ConfigureAwait(false);
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"OpenWeatherMap request failed with status code {(int)response.StatusCode}: {content}");
        }

        var forecastResponse = JsonSerializer.Deserialize<OpenWeatherForecastResponse>(
            content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (forecastResponse?.City is null || forecastResponse.List is null)
        {
            throw new InvalidOperationException("The weather forecast response was empty or invalid.");
        }

        var timezoneOffset = TimeSpan.FromSeconds(forecastResponse.City.Timezone);
        var dailyForecasts = forecastResponse.List
            .Where(item => item.Main is not null)
            .Select(item => new LocalForecastEntry(item, ConvertToLocalDateTime(item.UnixTimestamp, timezoneOffset)))
            .GroupBy(entry => DateOnly.FromDateTime(entry.LocalDateTime))
            .OrderBy(group => group.Key)
            .Take(days)
            .Select(group => MapDayForecast(group.Key, SelectMiddayEntry(group)))
            .ToList();

        if (dailyForecasts.Count == 0)
        {
            throw new InvalidOperationException("No forecast data was returned by OpenWeatherMap.");
        }

        return new WeatherResponse
        {
            City = forecastResponse.City.Name ?? city,
            Country = forecastResponse.City.Country ?? string.Empty,
            Forecasts = dailyForecasts
        };
    }

    private static string BuildRequestUri(string city, string apiKey)
    {
        return $"data/2.5/forecast?q={Uri.EscapeDataString(city)}&units=metric&appid={Uri.EscapeDataString(apiKey)}";
    }

    private static DateTime ConvertToLocalDateTime(long unixTimestamp, TimeSpan timezoneOffset)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).ToOffset(timezoneOffset).DateTime;
    }

    private static OpenWeatherForecastItem SelectMiddayEntry(IEnumerable<LocalForecastEntry> entries)
    {
        return entries
            .OrderBy(entry => Math.Abs((entry.LocalDateTime.TimeOfDay - Midday).TotalMinutes))
            .Select(entry => entry.Forecast)
            .First();
    }

    private static DayForecast MapDayForecast(DateOnly date, OpenWeatherForecastItem item)
    {
        var main = item.Main ?? throw new InvalidOperationException("Forecast data is missing the main weather section.");
        var condition = item.Weather.FirstOrDefault();

        return new DayForecast
        {
            Date = date,
            TemperatureCelsius = main.Temperature,
            TemperatureFeelsLike = main.FeelsLike,
            Description = condition?.Description ?? string.Empty,
            Icon = condition?.Icon ?? string.Empty,
            Humidity = main.Humidity,
            WindSpeed = item.Wind?.Speed ?? 0
        };
    }

    private sealed record LocalForecastEntry(OpenWeatherForecastItem Forecast, DateTime LocalDateTime);

    private sealed class OpenWeatherForecastResponse
    {
        [JsonPropertyName("city")]
        public OpenWeatherCity? City { get; set; }

        [JsonPropertyName("list")]
        public List<OpenWeatherForecastItem>? List { get; set; }
    }

    private sealed class OpenWeatherCity
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }
    }

    private sealed class OpenWeatherForecastItem
    {
        [JsonPropertyName("dt")]
        public long UnixTimestamp { get; set; }

        [JsonPropertyName("main")]
        public OpenWeatherMain? Main { get; set; }

        [JsonPropertyName("weather")]
        public List<OpenWeatherCondition> Weather { get; set; } = [];

        [JsonPropertyName("wind")]
        public OpenWeatherWind? Wind { get; set; }
    }

    private sealed class OpenWeatherMain
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    private sealed class OpenWeatherCondition
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
    }

    private sealed class OpenWeatherWind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
    }
}
