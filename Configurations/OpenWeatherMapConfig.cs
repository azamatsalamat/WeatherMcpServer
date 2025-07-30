using Microsoft.Extensions.Logging;

public class OpenWeatherMapConfig
{
    public string ApiKey { get; }
    public string BaseUrl { get; }

    public OpenWeatherMapConfig(ILogger<OpenWeatherMapConfig> logger)
    {
        ApiKey = Environment.GetEnvironmentVariable("OPENWEATHERMAP_API_KEY");
        BaseUrl = Environment.GetEnvironmentVariable("OPENWEATHERMAP_BASE_URL");

        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            logger.LogError("API key for OpenWeatherMap is not configured.");
            throw new InvalidOperationException("API key for OpenWeatherMap is not configured.");
        }
        if (string.IsNullOrWhiteSpace(BaseUrl))
        {
            logger.LogError("Base URL for OpenWeatherMap is not configured.");
            throw new InvalidOperationException("Base URL for OpenWeatherMap is not configured.");
        }
    }
}