# CentrelinkDataServicesWeather

Read CDS.Weather/README.txt for instructions.

This solution includes a single microservice (CDS.Weather), unit and integration tests following a Test Driven Development (TDD) approach. Unit test doubles demonstate both stubs and mocks. Dependency injection has been used in Program.cs.
A Dockerfile exists to create the image.
A kubernetes Deployment.yaml and Service.yaml are also included. Multiple manifest files can be applied by using kubectl to the path the manifest files reside in.
kubectl apply -f /path/to/manifests

TODO: Helm Charts are a more sustainable means to deploy in multiple SDLC environments.

Candidate test double faker Nuget packages have also been included for unit testing. Also, serves as a reminder regarding which Nuget packages are required. 

* dotnet add package OpenWeatherMapSharp
* dotnet add package NSubstitute
* dotnet add package NSubstitute.Analyzers.CSharp

It should be possible to use EntityFrameworkCore InMemory with PostGreSQL or Mongo for JSON document handling
* dotnet add package Microsoft.EntityFrameworkCore.InMemory
* dotnet add package RabbitMQ.Fakes.DotNetStandard --version 2.2.1
* dotnet add package WebMotions.Fake.Authentication.JwtBearer --version 8.0.1
* dotnet add package System.Net.Http.Json

TODO: All this could be implemented in a GitLab CI/CD pipeline to

* Build the CDSWeather project
* Run the xUnit and Integration tests.
* On test success, create a Docker image.
* Deploy that image on Kubernetes and create a K8 service.

TDD Pillars withing Agile XP Framework [https://en.wikipedia.org/wiki/Extreme_programming]
* Test first
* Red, Green, Refactor (RGR)
  
Describe each software feature as a story. That story has a title, description, acceptance criteria and story points. Firstly, design all the necessary class structures from a client perspective. Secondly, write all the unit tests. Thirdly, write the implementation code. Fourthly, RGR until green for all unit tests. Move onto next feature and repeat process.

All the elements are present.

Enjoy
