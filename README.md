# CentrelinkDataServicesWeather

Read CDS.Weather/README.txt for instructions.

This solution has unit and integration tests following a Test Driven Development approach. Unit test demonstate both stubs and mocks. Dependency injection has been used in Program.cs.
A Dockerfile exists to create the image.
A kubernetes Deployment.yaml and Service.yaml is also included.
Candidate faker Nuget packages have also been included for unit testing. Also, servers as a reminder regarding which Nuget packages are required. 

dotnet add package OpenWeatherMapSharp
dotnet add package NSubstitute
dotnet add package NSubstitute.Analyzers.CSharp

It should be possible to use EntityFrameworkCore InMemory with PostGreSQL or Mongo for JSON document handling
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package RabbitMQ.Fakes.DotNetStandard --version 2.2.1
dotnet add package WebMotions.Fake.Authentication.JwtBearer --version 8.0.1
dotnet add package System.Net.Http.Json

TODO: All this could be implemented in a GitLab CI/CD pipeline to

Build the CDSWeather project
Run the xUnit and Integration tests.
On test success, create a Docker image.
Deploy that image on Kubernetes and create a K8 service.

All the elements are present.

Enjoy
