
# Weather MCP Server Documentation

## Overview

The Weather MCP Server is a .NET 8 application that exposes MCP tools for accessing real-time weather data, forecasts, and alerts using the OpenWeatherMap API. It is designed for integration with AI assistants and MCP clients.

## MCP Tools

### 1. GetCurrentWeather

**Description:**  
Retrieves the current weather conditions for a specified city.

**Signature:**  
`GetCurrentWeather(string city, string? countryCode = null)`

**Parameters:**  
- `city`: The name of the city (e.g., "London")
- `countryCode` (optional): The country code (e.g., "GB", "US")

**Returns:**  
A summary of the current weather (temperature, conditions)

---

### 2. GetWeatherForecast

**Description:**  
Provides a weather forecast for a specified city for a given number of days (minimum 3-day forecast).

**Signature:**  
`GetWeatherForecast(string city, string? countryCode = null, int days = 3)`

**Parameters:**  
- `city`: The name of the city
- `countryCode` (optional): The country code
- `days`: Number of days for the forecast (default: 7)

**Returns:**  
A daily forecast summary (temperature, conditions, etc.)

---

### 3. GetWeatherAlerts

**Description:**  
Returns weather alerts or warnings for a specified city (if available).

**Signature:**  
`GetWeatherAlerts(string city, string? countryCode = null)`

**Parameters:**  
- `city`: The name of the city
- `countryCode` (optional): The country code

**Returns:**  
A list of active weather alerts or warnings.

---

## Setup & Launch Instructions (VS Code)

### 1. Clone the repository
```powershell
git clone https://github.com/azamatsalamat/WeatherMcpServer
cd WeatherMcpServer
```

### 2. Open the project in VS Code
- Launch VS Code and open the `WeatherMcpServer` folder.

### 3. Configure your API key using `.vscode/mcp.json`
Create a file named `.vscode/mcp.json` in the project root with the following content:
```json
{
  "servers": {
    "WeatherMcpServer": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "WeatherMcpServer.csproj"
      ],
      "env": {
        "OPENWEATHERMAP_API_KEY": "<your-api-key>",
        "OPENWEATHERMAP_BASE_URL": "https://api.openweathermap.org"
      }
    }
  }
}
```
Insert your own OpenWeatherMap API key.

### 4. Build project
Open the integrated terminal in VS Code and run:
```powershell
dotnet build
```

### 5. Launch the MCP server
- Press Start button in mcp.json above the "WeatherMcpServer" section.

### 6. Interact with the MCP tools
Use an MCP client or AI assistant (e.g., CoPilot) to call the tools described above.

## Troubleshooting

- Ensure your API key is valid and present in `.vscode/mcp.json`
- Check the `logs/` directory for error details
- Make sure .NET 8.0 is installed

## References

- [OpenWeatherMap API Documentation](https://openweathermap.org/api)
- [MCP .NET Documentation](https://learn.microsoft.com/dotnet/ai/quickstarts/build-mcp-server)
