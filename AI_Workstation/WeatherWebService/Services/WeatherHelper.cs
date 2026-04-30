using WeatherWebService.Models;

namespace WeatherWebService.Services;

internal static class WeatherHelper
{
    private static readonly TimeSpan Midday = TimeSpan.FromHours(12);

    public static string BuildRequestUri(string city, string apiKey)
    {
        return $"data/2.5/forecast?q={Uri.EscapeDataString(city)}&units=metric&appid={Uri.EscapeDataString(apiKey)}";
    }

    public static DateTime ConvertToLocalDateTime(long unixTimestamp, TimeSpan timezoneOffset)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).ToOffset(timezoneOffset).DateTime;
    }

    public static OpenWeatherForecastItem SelectMiddayEntry(IEnumerable<LocalForecastEntry> entries)
    {
        return entries
            .OrderBy(entry => Math.Abs((entry.LocalDateTime.TimeOfDay - Midday).TotalMinutes))
            .Select(entry => entry.Forecast)
            .First();
    }

    public static DayForecast MapDayForecast(DateOnly date, OpenWeatherForecastItem item)
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
}
