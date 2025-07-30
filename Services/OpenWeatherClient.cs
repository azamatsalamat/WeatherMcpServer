using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using CSharpFunctionalExtensions;

public class OpenWeatherClient : IWeatherClient
{
    private readonly OpenWeatherMapConfig _config;
    private readonly ILogger<OpenWeatherClient> _logger;
    private static readonly HttpClient httpClient = new HttpClient();

    public OpenWeatherClient(OpenWeatherMapConfig config, ILogger<OpenWeatherClient> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task<Result<OpenWeatherResponse>> GetWeatherAsync(string city, string country)
    {
        var url = $"{_config.BaseUrl}/data/2.5/weather?q={city},{country}&appid={_config.ApiKey}&units=metric";
        try
        {
            var response = await httpClient.GetFromJsonAsync<OpenWeatherResponse>(url);
            if (response == null || response.Weather == null || response.Weather.Length == 0)
            {
                _logger.LogWarning("Could not retrieve weather for {City}", city + ", " + country);
                return Result.Failure<OpenWeatherResponse>($"Could not retrieve weather for {city}, {country}.");
            }
            return Result.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get weather data for {City}", city + ", " + country);
            return Result.Failure<OpenWeatherResponse>($"Failed to get weather data for {city}, {country}.");
        }
    }

    public async Task<Result<OpenWeatherForecastResponse>> GetWeatherForecastAsync(string city, string? country = null, int days = 5)
    {
        var url = $"{_config.BaseUrl}/data/2.5/forecast?q={city},{country}&appid={_config.ApiKey}&units=metric";
        try
        {
            var response = await httpClient.GetFromJsonAsync<OpenWeatherForecastResponse>(url);
            if (response == null || response.List == null || response.List.Count == 0)
            {
                _logger.LogWarning("Could not retrieve weather forecast for {City}", city);
                return Result.Failure<OpenWeatherForecastResponse>($"Could not retrieve weather forecast for {city}.");
            }
            return Result.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get weather forecast for {City}", city);
            return Result.Failure<OpenWeatherForecastResponse>($"Failed to get weather forecast for {city}.");
        }
    }

    public async Task<Result<List<WeatherAlert>>> GetWeatherAlertsAsync(string city, string? country)
    {
        var coordUrl = $"{_config.BaseUrl}/data/2.5/weather?q={city},{country}&appid={_config.ApiKey}&units=metric";
        try
        {
            var coordResponse = await httpClient.GetFromJsonAsync<OpenWeatherResponse>(coordUrl);
            if (coordResponse == null || coordResponse.Coord == null)
            {
                _logger.LogWarning("Could not retrieve coordinates for {City}", city + ", " + country);
                return Result.Failure<List<WeatherAlert>>($"Could not retrieve coordinates for {city}, {country}.");
            }
            var lat = coordResponse.Coord.Lat;
            var lon = coordResponse.Coord.Lon;

            var alertsUrl = $"{_config.BaseUrl}/data/3.0/onecall?lat={lat}&lon={lon}&appid={_config.ApiKey}&units=metric";
            var alertsResponse = await httpClient.GetFromJsonAsync<OpenWeatherAlertsResponse>(alertsUrl);
            if (alertsResponse == null || alertsResponse.Alerts == null || alertsResponse.Alerts.Count == 0)
            {
                return Result.Success(new List<WeatherAlert>());
            }
            return Result.Success(alertsResponse.Alerts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get weather alerts for {City}", city);
            return Result.Failure<List<WeatherAlert>>($"Failed to get weather alerts for {city}.");
        }
    }
}
