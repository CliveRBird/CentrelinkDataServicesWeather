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
rem From package manager browsing the ALL web NuGet repos for AdamTibi.OpenWeather package

rem Now start to add the unit tests to the solution.
dotnet new xunit -o CDS.Weather.Tests.Unit -f net8.0
dotnet sln add CDS.Weather.Tests.Unit
dotnet add CDS.Weather.Tests.Unit reference CDS.Weather
rem run the single template empty test.
dotnet test

rem From package manager browsing the ALL web NuGet repos for NSubsitute package 

rem Using the Dockerfile, create the image and test it locally before deploying to K8 cluster.
cd C:\Users\Administrator\source\repos\CentrelinkDataServicesWeather
docker build -t CDSWeatherForecast-web-api .
docker run -d -p 5001:80 — name CDSWeatherForecast-web-api-container CDSWeatherForecast-web-api
docker ps
curl http://localhost:5001/swagger/index.html

rem See K8 Deployment.yaml and Service.yaml manifests to kubernetes deployment.

