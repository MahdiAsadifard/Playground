namespace WeatherWebService.Models;

internal sealed record LocalForecastEntry(OpenWeatherForecastItem Forecast, DateTime LocalDateTime);
