using CSharpFunctionalExtensions;

public interface IWeatherProvider
{
    Task<Result<string>> GetWeatherAsync(string city, string? country);
    Task<Result<string>> GetWeatherForecastAsync(string city, string? country, int days);
    Task<Result<string>> GetWeatherAlertsAsync(string city, string? country);
}