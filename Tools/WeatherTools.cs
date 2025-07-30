using System.ComponentModel;
using ModelContextProtocol.Server;

public class WeatherTools
{
    private readonly IWeatherProvider _weatherProvider;

    public WeatherTools(IWeatherProvider weatherProvider)
    {
        _weatherProvider = weatherProvider;
    }

    [McpServerTool]
    [Description("Retrieves current weather in the provided city.")]
    public async Task<string> GetCurrentWeather(
        [Description("Name of the city to return weather for")] string city,
        [Description("Optional: country code (e.g. 'US')")] string? country = null)
    {
        var result = await _weatherProvider.GetWeatherAsync(city, country);
        if (result.IsFailure)
        {
            return result.Error;
        }

        return result.Value;
    }

    [McpServerTool]
    [Description("Retrieves weather forecast in the provided city for next few days.")]
    public async Task<string> GetWeatherForecast(
        [Description("Name of the city to return weather for")] string city,
        [Description("Optional: country code (e.g. 'US')")] string? country = null,
        [Description("Optional: number of days for forecast")] int days = 5)
    {
        var result = await _weatherProvider.GetWeatherForecastAsync(city, country, days);
        if (result.IsFailure)
        {
            return result.Error;
        }

        return result.Value;
    }

    [McpServerTool]
    [Description("Retrieves weather alerts in the provided city.")]
    public async Task<string> GetWeatherAlerts(
        [Description("Name of the city to return weather for")] string city,
        [Description("Optional: country code (e.g. 'US')")] string? country = null)
    {
        var result = await _weatherProvider.GetWeatherAlertsAsync(city, country);
        if (result.IsFailure)
        {
            return result.Error;
        }

        return result.Value;
    }
}