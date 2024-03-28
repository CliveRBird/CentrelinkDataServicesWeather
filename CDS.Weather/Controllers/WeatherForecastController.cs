using Microsoft.AspNetCore.Mvc;
using OpenWeatherMapSharp;

// See url below on how to acquire a free api key.
// https://www.nuget.org/packages/OpenWeatherMapSharp
using OpenWeatherMapSharp.Models;

namespace CDS.Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private const int FORECAST_DAYS = 5;
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _config;
    private const string OPENWEATHERMAPAPIKEY = "OWMAPIKEY";

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    [HttpGet("ConvertCToF")]
    public double ConvertCToF(double c)
    {
        double f = c * (9d / 5d) + 32;
        _logger.LogInformation("conversion requested");
        return f;
    }

    [HttpGet("GetRealWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetReal()
    {
        const decimal GREENWICH_LAT = 51.4810m;
        const decimal GREENWICH_LON = 0.0052m;
        string apiKey = _config["OpenWeather:Key"];
        HttpClient httpClient = new HttpClient();
        
        OpenWeatherMapService service = new(OPENWEATHERMAPAPIKEY);
        double latitude = 51.4810;
        double longitude = 0.0052;
        // TODO: The real API weather service doesn't have to work for the project.
        OpenWeatherMapServiceResponse<WeatherRoot> serviceResponse = await service.GetWeatherAsync(latitude, longitude);
        /*
        Client openWeatherClient = new Client(apiKey, httpClient);
        OneCallResponse res = await openWeatherClient.OneCallAsync
            (GREENWICH_LAT, GREENWICH_LON, new[] {
                Excludes.Current, Excludes.Minutely,
                Excludes.Hourly, Excludes.Alerts }, Units.Metric);
        */
        WeatherForecast[] wfs = new WeatherForecast[FORECAST_DAYS];
        /*
        for (int i = 0; i < wfs.Length; i++)
        {
            var wf = wfs[i] = new WeatherForecast();
            wf.Date = res.Daily[i + 1].Dt;
            double forecastedTemp = res.Daily[i + 1].Temp.Day;
            wf.TemperatureC = (int)Math.Round(forecastedTemp);
            wf.Summary = MapFeelToTemp(wf.TemperatureC);
        }
        */
        return wfs;
       
    }

    [HttpGet("GetRandomWeatherForecast")]
    public IEnumerable<WeatherForecast> GetRandom()
    {
        WeatherForecast[] wfs = new WeatherForecast[FORECAST_DAYS];
        for (int i = 0; i < wfs.Length; i++)
        {
            var wf = wfs[i] = new WeatherForecast();
            wf.Date = DateTime.Now.AddDays(i + 1);
            wf.TemperatureC = Random.Shared.Next(-20, 55);
            wf.Summary = MapFeelToTemp(wf.TemperatureC);
        }
        return wfs;
    }

    private static string MapFeelToTemp(int temperatureC)
    {
        // Anything <= 0 is "Freezing"
        if (temperatureC <= 0)
        {
            return Summaries.First();
        }
        // Dividing the temperature into 5 intervals
        int summariesIndex = (temperatureC / 5) + 1;
        // Anything >= 45 is "Scorching"
        if (summariesIndex >= Summaries.Length)
        {
            return Summaries.Last();
        }
        return Summaries[summariesIndex];
    }
}