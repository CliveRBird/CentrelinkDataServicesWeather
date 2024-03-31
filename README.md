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

It should be possible to use EntityFrameworkCore InMemory with PostGreSQL or Mongo for JSON document handling. RabbitMQ is a consideration for a pub/sub event bus and a queue.
* dotnet add package Microsoft.EntityFrameworkCore.InMemory
* dotnet add package RabbitMQ.Fakes.DotNetStandard --version 2.2.1
* dotnet add package WebMotions.Fake.Authentication.JwtBearer --version 8.0.1
* dotnet add package System.Net.Http.Json

To generate fake data install Faker.Net
* dotnet add package Faker.Net

Required Visual Studio 2022 Plugins
* Fine Code Coverage (FCC). Required for code coverage inspection. Thus ensuring all production code is covered by a unit test.

TODO: Attain iteration zero. Implement in a GitLab CI/CD pipeline to

* Build the CDSWeather project
* Run the xUnit and Integration tests.
* On test success, create a Docker image.
* Deploy that image on Kubernetes and create a K8 service.

Test Driven Development (TDD) Pillars within Agile XP Framework [https://en.wikipedia.org/wiki/Extreme_programming]
* Test first
* Red, Green, Refactor (RGR)
  
Describe each software feature as a story. That story has a title, description, acceptance criteria and story points. Firstly, design all the necessary class structures from a client (a caller) perspective. Secondly, write all the unit tests. Thirdly, write the implementation code. Fourthly, RGR until green for all unit tests. Move onto next feature and repeat process. For each identified dependency, create a test double in either a stubbed or mocked format. Only use a single test double style across the entire solution.

Domain Driven Design (DDD) Summary

![image](https://github.com/CliveRBird/CentrelinkDataServicesWeather/assets/90135557/e4d5b85b-5eb3-4913-9cdc-74f28379f9d0)

Structure and arrange the Visual Studio Solution such that its multiple projects are arranged as follows

* CDS.WeatherForecast.Contract
* CDS.WeatherForecast.Domain.Tests.Unit
* CDS.WeatherForecast.Domain
* CDS.WeatherForecast.WebApi
* CDS.WeatherForecast.Website

Having the contents in each project as described below 

* Contracts (written in *.cs files): This is what the outside world sees. These contracts represent the shape of the data that will be exchanged between the backend and the client. The client should know the data elements of the contract, so it knows what to expect from a headless microservice.
* Entities (written in *.cs files): The domain objects with identities.
* Value Objects (written in *.cs files): The domain objects that don’t require an identity.
* Domain Objects (written in *.cs files): This is the group of entities and value objects in the system.
* Repositories (written in *.cs files): These are the classes that will save and load data from a data store (relational DB, document DB, file system, blob storage, etc). Benefit being Domain Objects use DI repository injection decoupling entities and value objects from their architectural implementation (e.g. Mongodb.)
* Domain Services (written in *.cs files): This is where the business logic manifests, and it will interact with the repositories for CRUD operations. These services are not exposed to the outside world.
* Application Services (written in *.cs files) {WebApi}: Controllers in basic scenarios act as application services where they interact with domain services to serve a REST request. Application services are exposed to the outside world.

Noting, Domain Objects and Domain Services are both under CDS...Domain. The WeatherForecast solution would require a more complex problem to better highlight DDD in practice. 

All the elements are present.

Enjoy

Freezing time for time simulation. Possible use case would be for the unit test to iterate for a time range for SUT simulation purposes.

```
public interface INowWrapper
{
    DateTime Now { get; }
}

public class NowWrapper : INowWrapper
{
    public DateTime Now => DateTime.Now;
}
```

This is a wrapper to allow injecting the current time as a dependency. To register the wrapper in Program.cs:

```
builder.Services.AddSingleton<INowWrapper>(_ => new NowWrapper());
```

A, single service class, provides the service

```
public class MyService
{

    private readonly INowWrapper _nowWrapper;

    public MyService(INowWrapper nowWrapper) 
    {
      _nowWrapper = nowWrapper;
    }

    public DateTime GetTomorrow() => _nowWrapper.Now.AddDays(1).Date;
}
```

To inject the current time to the following unit test

```
public void GetTomorrow_NormalDay_TomorrowIsRight()
{

    // Arrange
    var today = new DateTime(2024, 4, 1);
    var expected = new DateTime(2024, 4, 2);
    var nowWrapper = Substitute.For<INowWrapper>();
    nowWrapper.Now.Returns(today);
    var myService = new MyService(nowWrapper);
    
    // Act
    var actual = myService.GetTomorrow();

    // Assert
    Assert.Equal(expected, actual);
}
```
