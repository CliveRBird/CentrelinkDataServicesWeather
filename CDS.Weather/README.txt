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

cd C:\Users\Administrator\source\repos\CentrelinkDataServicesWeather

rem From package manager browsing the ALL web NuGet repos for NSubsitute package 
rem Alternatively install the package using the line below. Analysers is optional.
dotnet add package NSubstitute
dotnet add package NSubstitute.Analyzers.CSharp

rem These two packages are for faking during unit testing.
rem It should be possible to use EF InMemory with PostGreSQL or Mongo for JSON document handling
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package RabbitMQ.Fakes.DotNetStandard --version 2.2.1
rem JWT Faker for unit and integration testing.
rem See URL https://github.com/webmotions/fake-authentication-jwtbearer
dotnet add package WebMotions.Fake.Authentication.JwtBearer --version 8.0.1

rem Integration testing
cd C:\Users\Administrator\source\repos\CentrelinkDataServicesWeather
dotnet new xunit -o CDS.Weather.Tests.Integration -f net8.0
dotnet sln add CDS.Weather.Tests.Integration
rem Integration testing will seriliase and deserialise JSON over HTTP. Thus add the Nuget Package below.
cd CDS.Weather.Tests.Integration
dotnet add package System.Net.Http.Json

rem Using the Dockerfile, create the image and test it locally before deploying to K8 cluster.
cd C:\Users\Administrator\source\repos\CentrelinkDataServicesWeather
docker build -t CDSWeatherForecast-web-api .
docker run -d -p 5001:80 — name CDSWeatherForecast-web-api-container CDSWeatherForecast-web-api
docker ps
curl http://localhost:5001/swagger/index.html

rem See K8 Deployment.yaml and Service.yaml manifests to kubernetes deployment.
Multiple K8 manifest files can be applied by using kubectl to the path the manifest files reside in.
kubectl apply -f /path/to/manifests

rem TODO: Helm Charts are a more sustainable means to deploy in multiple SDLC environments.