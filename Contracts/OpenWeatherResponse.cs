using System.Text.Json.Serialization;

public record OpenWeatherResponse
{
    public WeatherInfo[] Weather { get; set; }

    [JsonPropertyName("main")]
    public MainInfo MainInfo { get; set; }
    public Coord? Coord { get; set; }
}

public class Coord
{
    public float Lat { get; set; }
    public float Lon { get; set; }
}