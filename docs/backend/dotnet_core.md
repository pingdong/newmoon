[Back](../../README.md)

## Backend

The current version borrowed some codes and idea from Microsoft [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers) project, Thanks.
 
The below image suggests the high-level data flow, and an idea of the connection between concepts: CQRS, Mediator, Domain Event, Integration Event, Event Source.
![](./dotnet_core/CQRS.png)
 
In this project, a few advance technology, e.g. Reflection, was used to comply with CoS concept. It may bring some difficulty to understand.


### The following libraries are used:
* [Microsoft ASP.Net Core 2.2.3](https://docs.microsoft.com/en-nz/aspnet/#pivot=core)
* [Microsoft OData 7.1.0](http://odata.github.io/)
* [GraphQL .Net 2.4.0](https://graphql-dotnet.github.io/)
* [GraphiQL 1.2.0](https://github.com/JosephWoodward/graphiql-dotnet)
* [StackExchange.Redis 1.2.6](https://github.com/StackExchange/StackExchange.Redis)
* [Dapper 1.60.1](https://github.com/StackExchange/Dapper) ([Tutorial](http://dapper-tutorial.net/dapper))
* [Microsoft Entity Framework Core 2.2.3](https://docs.microsoft.com/en-us/ef/#pivot=efcore)
* [Autofac 4.9.1](https://autofac.org/)
* [AutoMapper 8.0.0](https://automapper.org/)
* [FluentValidation 8.1.3](https://fluentvalidation.net/)
* [MediatR 6.0.0](https://github.com/jbogard/MediatR)
* [Serilog.AspNetCore 2.1.1](https://serilog.net/)
* [Polly 7.1.0](http://www.thepollyproject.org/)
* [NCrontab 3.3.1](https://github.com/atifaziz/NCrontab)
* [Swashbuckle.AspNetCore 4.0.1](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [xUnit.net 2.4.1](https://xunit.github.io/)
* [Moq 4.10.1](https://github.com/moq/moq4)
* [Docker 18.06.0](https://www.docker.com/)
* [IdentityServer4 2.3.2](https://identityserver.io/)

## The following design patterns are involved:
* Clean Architect (aka Onion Architect)
* [Mediator](https://en.wikipedia.org/wiki/Mediator_pattern)
* [CQRS](https://martinfowler.com/bliki/CQRS.html)
* [Bounded Context](https://martinfowler.com/bliki/BoundedContext.html)
* [ValueObject](https://martinfowler.com/bliki/ValueObject.html) (Poor support in EF Core at present)
* UnitOfWork & Repository

### Concepts are covered:
* SoC 
* Domain Driven Design 
* Event Sourcing
  * Domain Event
  * Integration Event 
* Resiliency 
* Idempotency 
* REST vs OData vs [GraphQL](https://graphql-dotnet.github.io)

### Practices
* Microservice Architect 
* Multiple Configuration Source, Sturcture Application Settings
* Authentication & Authorization 
* Using InMemory Data Provider for testing

### Roadmap  
* Application
  * [x] Background Tasks 
  * [x] Integration Event
  * [ ] API Gateway 
  * [ ] ElasticSearch  
  * [ ] Grafana or Kibana
  * [ ] Prometheus
* Cache
  * [x] In-memory
  * [x] Redis
* Data
  * [x] SQL Server
  * [x] Entity Framework Core
  * [ ] NoSQL 
* WebAPI
  * [x] RESTful  
  * [x] OData  
  * [x] [GraphQL](./backend/dotnet_core/graphql.md)
* Testing
  * [x] Unit Testing
  * [x] Integration Testing
  * [x] Functional Testing
  * [ ] Performance and Load Testing
* Docker
  * [x] Docker 
  * [ ] Kubernetes  
  

### Asp.Net Core   
[Dependency Injection](./backend/dotnet_core/IoC.md)  
[Configuration](./backend/dotnet_core/Configuration.md)  
[Data Validation](./backend/dotnet_core/DataValidation.md)   
[Identity](./backend/dotnet_core/Identity.md)    
[HttpClient](./backend/dotnet_core/HttpClient.md)  
[Integration Event](./backend/dotnet_core/IntegrationEvent.md)  
[Background Tasks](./backend/dotnet_core/BackgroundTask.md)
[OData](./backend/dotnet_core/odata.md)

### Data Management  
[Cache](./backend/dotnet_core/Cache.md)  
[Mapping](./backend/dotnet_core/Mapping.md)  
[EF Core](./backend/dotnet_core/EFCore.md)  

### Testing  
[Testing](./backend/dotnet_core/Testing.md)