using CSharpFunctionalExtensions;

public interface IWeatherClient
{
    Task<Result<OpenWeatherResponse>> GetWeatherAsync(string city, string? country);
    Task<Result<OpenWeatherForecastResponse>> GetWeatherForecastAsync(string city, string? country,
    int days);
    Task<Result<List<WeatherAlert>>> GetWeatherAlertsAsync(string city, string? country);
}
