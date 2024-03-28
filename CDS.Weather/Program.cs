
/* 
 * 
 * 
 * 
 * Original default boilder template code on issuing   
 * 
 * cd C:\Users\Administrator\source\repos
 * md CentrelinkDataServicesWeather
 * cd CentrelinkDataServicesWeather
 * dotnet new sln
 * dotnet new webapi -o CDS.Weather -f net8.0
 * dotnet sln add CDS.Weather
 * 
 * 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/

using CDS.Weather.Wrappers;
using AdamTibi.OpenWeather;
using CDS.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IClient>(_ => {
    // Below is useful for load testing. It's undsireable to swamp the real API when load testing your app.
    // So use the new ClientStub() object to avoid invoking too many calls to the real Web site. Instead use
    // the local test environment. 
    bool isLoad = bool.Parse(builder.Configuration["LoadTest:IsActive"]);
    if (isLoad) return new ClientStub();
    else
    {
        string apiKey = builder.Configuration["OpenWeather:Key"];
        HttpClient httpClient = new HttpClient();
        return new Client(apiKey, httpClient);
    }
});
builder.Services.AddSingleton<INowWrapper>(_ => new NowWrapper());
builder.Services.AddTransient<IRandomWrapper>(_ => new RandomWrapper());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();