rem
rem How to create the solution and project from a cmd prompt.
rem

cd C:\Users\Administrator\source\repos
md CentrelinkDataServicesWeather
cd CentrelinkDataServicesWeather
dotnet new sln
dotnet new webapi -o CDS.Weather -f net8.0
dotnet sln add CDS.Weather


rem From VS Tools, NuGet Package Manager runn the command below.
NuGet\Install-Package OpenWeatherMapSharp -Version 3.1.4


