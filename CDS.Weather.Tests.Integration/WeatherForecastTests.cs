using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http.Json;

namespace CDS.Weather.Tests.Integration;

// To run the integration test right click on CDS.Weather in solution explorer. Select
// Debug. Debug -> start without debugging. This will open the a console running Kestral web server
// plus the application CDS.Weather.

public class WeatherForecastTests
{
    private const string BASE_ADDRESS = "https://localhost:7225";
    // In reality the test should exercise the Http request over the internet to get the real weather.
    // I've not applied to the free access token. Simply for demonstration purposes, used Random over Real.
    // The point being this integration test would exercise the http request over the internet.
    // private const string API_URI = "/WeatherForecast/GetRealWeatherForecast";
    private const string API_URI = "/WeatherForecast/GetRandomWeatherForecast";
    private record WeatherForecast(DateTime Date, int TemperatureC, int TemperatureF, string? Summary);

    // Must run CDS.Weather before running this test
    [Fact]
    public async Task GetRealWeatherForecast_Execute_GetNext5Days()
    {
        
        // Arrange
        var today = DateTime.Now.Date;
        var next5Days = new[] { today.AddDays(1), today.AddDays(2),
            today.AddDays(3), today.AddDays(4), today.AddDays(5) };
        HttpClient httpClient = new HttpClient { BaseAddress = new Uri(BASE_ADDRESS) };

        // Act
        var httpRes = await httpClient.GetAsync(API_URI);

        // Assert
        var wfs = await httpRes.Content.ReadFromJsonAsync<WeatherForecast[]>();
        for (int i = 0; i < 5; i++)
        {
            Assert.Equal(next5Days[i], wfs[i].Date.Date);
        }
        
    }


}