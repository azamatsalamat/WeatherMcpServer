using System;
using System.Collections.Generic;

public class OpenWeatherForecastResponse
{
    public List<ForecastItem> List { get; set; } = new();
    public CityInfo City { get; set; } = new();
}

public class ForecastItem
{
    public string Dt_txt { get; set; } // keep as string, parse to DateTime in code
    public MainInfo Main { get; set; } = new();
    public List<WeatherInfo> Weather { get; set; } = new();
}

public class CityInfo
{
    public string Name { get; set; }
    public string Country { get; set; }
}
