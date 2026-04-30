namespace WeatherWebService.Models;

public sealed class DayForecast
{
    public DateOnly Date { get; set; }

    public double TemperatureCelsius { get; set; }

    public double TemperatureFeelsLike { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public int Humidity { get; set; }

    public double WindSpeed { get; set; }
}
