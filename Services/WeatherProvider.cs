using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

public class WeatherProvider: IWeatherProvider
{
    private readonly IWeatherClient _weatherClient;
    private readonly ILogger<WeatherProvider> _logger;

    public WeatherProvider(IWeatherClient weatherClient, ILogger<WeatherProvider> logger)
    {
        _weatherClient = weatherClient;
        _logger = logger;
    }

    public async Task<Result<string>> GetWeatherAsync(string city, string? country = null)
    {
        _logger.LogInformation("Starting to get current weather for {City}", city + ", " + country);
        var result = await _weatherClient.GetWeatherAsync(city, country);
        if (result.IsFailure)
        {
            return result.Error;
        }

        var response = result.Value;
        var description = response.Weather[0].Description;
        var temp = response.MainInfo.Temp;
        return $"The current weather in {city} is {description} with a temperature of {temp}°C.";
    }

    public async Task<Result<string>> GetWeatherForecastAsync(string city, string? country, int days)
    {
        _logger.LogInformation("Starting to get weather forecast for {City}", city + ", " + country);
        var result = await _weatherClient.GetWeatherForecastAsync(city, country, days);
        if (result.IsFailure)
        {
            return result.Error;
        }

        var response = result.Value;
        var grouped = response.List
            .Select(item => new { 
                Date = DateTime.Parse(item.Dt_txt), 
                item.Main.Temp, 
                Description = item.Weather.Count > 0 ? item.Weather[0].Description : "No description" 
            })
            .GroupBy(x => x.Date.Date)
            .Take(days)
            .Select(g => new {
                Date = g.Key,
                MaxTemp = g.Max(x => x.Temp),
                MinTemp = g.Min(x => x.Temp),
                Description = g.First().Description
            });

        var forecastSummary = string.Join("\n", grouped.Select(day =>
            $"{day.Date:yyyy-MM-dd}: {day.Description}, Max temp: {day.MaxTemp:F1}°C, Min temp: {day.MinTemp:F1}°C"));
        
        return $"Weather forecast for {city} for the next {days} days:\n{forecastSummary}.";
    }

    public async Task<Result<string>> GetWeatherAlertsAsync(string city, string? country = null)
    {
        _logger.LogInformation("Starting to get weather alerts for {City}", city + ", " + country);
        var result = await _weatherClient.GetWeatherAlertsAsync(city, country);
        if (result.IsFailure)
        {
            return result.Error;
        }

        var alerts = string.Join("\n", result.Value.Select(a => 
            $"{a.Event} from {DateTimeOffset.FromUnixTimeSeconds(a.Start):yyyy-MM-dd HH:mm} to {DateTimeOffset.FromUnixTimeSeconds(a.End):yyyy-MM-dd HH:mm}"));
        return $"Weather alerts for {city}:\n{alerts}.";
    }
}