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
rem from package manager browsing the ALL web NuGet repos for AdamTibi.OpenWeather package

rem Now start to add the unit tests to the solution.
dotnet new xunit -o CDS.Weather.Tests.Unit -f net8.0
dotnet sln add CDS.Weather.Tests.Unit
dotnet add CDS.Weather.Tests.Unit reference CDS.Weather


