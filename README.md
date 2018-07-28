# **Newmoon**

For many reasons, I have seen many compromised designs/projects. Therefore I decided to implement a proof-of-concept solution and update along with emerging technology.

The current version borrowed some codes and idea from Microsoft eShopContainers project, Thanks.

I added some features that I believe they are important and helpful in real life. For example, Extensions and Reflections are widely used here, I know it's controversial, to generate lean, modular and reusable codes. The drawback is that may bring some difficulties to understand, especially to junior dev. But for me, long-term maintainability, flexibility and simplicity is always first priority. 

## The following libraries are used:
* Microsoft ASP.Net Core 2.1.1
* Microsoft Entity Framework Core 2.1.1
* Microsoft OData 7.0.0
* StackExchange.Redis 1.2.6
* Dapper 1.50.5
* Autofac 4.8.1
* AutoMapper 7.0.1
* FluentValidation 7.6.104
* MediatR 5.0.1
* Serilog.AspNetCore 2.1.1
* Polly 6.0.1
* Swashbuckle.AspNetCore 3.0.0
* xUnit.net 2.3.1
* Moq 4.9.0  

## The following design patterns are involved:
* Clean Architect
* Mediator
* CQRS
* UnitOfWork
* ValueObject (Poor support in EF Core Currently)
* Repository
* Specification 

## Other concepts are covered:
* SoC 
* BDD 
* Bounded Context 
* Domain Event
* Integration Event 
* Resiliency 
* Idempotency 

## Practices
* Using InMemory Data Provider for testing
  

> ## Roadmap

  
* [x] Microservice Coding Practice
* [x] Multiple Configuration Source, Sturcture Application Settings
* [x] Authentication & Authorization

* [x] Cache: In-memory, Redis
* [x] OData
* [ ] NoSQL
* [ ] ElasticSearch  
* [ ] GraphQL
  
* [ ] API Gateway
* [*] Integration Event
* [ ] Docker & Kubernetes
* [ ] Grafana or Kibana
* [ ] Prometheus
 
* [ ] Client @ React
* [ ] Client @ Razor

* [x] Testing
  * [x] Unit Testing
  * [x] Integration Testing
  * [x] Functional Testing
  * [ ] Performance and Load Testing


> ## Notes:

### [Setup](./docs/Setup.md)  
### [Backlog](./docs/Backlog.md)

### Asp.Net Core   
[Dependency Injection](./docs/IoC.md)  
[Configuration](./docs/Configuration.md)  
[Data Validation](./docs/DataValidation.md)  
[Identity](./docs/Identity.md)    
[HttpClient](./docs/HttpClient.md)
[Integration Event](./docs/IntegrationEvent.md)

### Data Management  
[Cache](./docs/Cache.md)  
[Mapping](./docs/Mapping.md)  
[EF Core](./docs/EFCore.md)  

### Testing  
[Testing](./docs/Testing.md)  

---
[Markdown cheat sheet ](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet)