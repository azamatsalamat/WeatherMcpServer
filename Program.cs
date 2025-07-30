using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("logs/mcp-logs.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .CreateLogger();

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);


builder.Services.AddSingleton(sp =>
{
    var logger = sp.GetRequiredService<ILogger<OpenWeatherMapConfig>>();
    return new OpenWeatherMapConfig(logger);
});
builder.Services.AddSingleton<IWeatherClient, OpenWeatherClient>();
builder.Services.AddSingleton<IWeatherProvider, WeatherProvider>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<WeatherTools>();

await builder.Build().RunAsync();