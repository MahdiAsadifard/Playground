using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherWebService.Models;

namespace WeatherWebService.Services;

public sealed class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherMapOptions _options;

    public WeatherService(HttpClient httpClient, IOptions<OpenWeatherMapOptions> options)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(options);

        _httpClient = httpClient;
        _options = options.Value;
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

        if (days < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(days), "Days must be at least 1.");
        }

        var apiKey = _options.ApiKey;
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("A valid OpenWeatherMap API key is required. Configure it in appsettings.json under 'OpenWeatherMap:ApiKey'.");
        }

        using var response = await _httpClient.GetAsync(WeatherHelper.BuildRequestUri(city, apiKey)).ConfigureAwait(false);
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
            .Select(item => new LocalForecastEntry(item, WeatherHelper.ConvertToLocalDateTime(item.UnixTimestamp, timezoneOffset)))
            .GroupBy(entry => DateOnly.FromDateTime(entry.LocalDateTime))
            .OrderBy(group => group.Key)
            .Take(days)
            .Select(group => WeatherHelper.MapDayForecast(group.Key, WeatherHelper.SelectMiddayEntry(group)))
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
}
