# **Newmoon**

For many reasons, I have seen many compromised designs/projects. Therefore I decided to implement an ideal solution as a template and maintain it to catch up with emerging technology and concepts in the future.

The current version borrowed some codes and idea from Microsoft eShopContainers project, Thanks.

On top of eShopContainers, I added some features that I believe they are important and helpful in real life. For example, Extensions and Reflections are widely used here, I know it's controversial, to generate lean, modular and reusable codes. This drawback is that may bring some difficulties to understand, especially to junior dev. To me, long-term maintainability, flexibility and simplicity is always crucial. 

## The following libraries are used:
* Microsoft ASP.Net Core 2.1
* Microsoft Entity Framework Core 2.1
* Microsoft OData 7.0.0
* Dapper 1.50.5
* Autofac 4.8.1
* AutoMapper 7.0.1
* FluentValidation 7.6
* MediatR 5.0.1
* Serilog.AspNetCore 2.1.1
* Polly 6.0.1
* Swashbuckle.AspNetCore 2.5.0
* xUnit.net 2.3.1
* Moq 4.8.3 

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

### Done: 
  
* [x] Microservice Coding Practice 
* [x] Multiple Configuration Source, Sturcture Application Settings 
* [x] OData 
* [x] Testing 
  * [x] Unit Testing 
  * [x] Integration Testing  
  * [x] Functional Testing  

### Not yet:
  
* [ ] Authentication & Authorization 
* [ ] API Gateway 
* [ ] GraphQL 
* [ ] Integration Message 
* [ ] Docker & Kubernetes 
* [ ] Client @ React 
* [ ] Client @ MVC 
* [ ] NoSQL & Redis 
* [ ] Performance and Load Testing  


> ## Backlog 
* [OData] The workaround of the compatibility with Swagger  
* [ValueObject] EF Core doesn't support inject value object in EF 2.1  
[Issue 12078] (https://github.com/aspnet/EntityFrameworkCore/issues/12078)  
[Issue 9148] (https://github.com/aspnet/EntityFrameworkCore/issues/9148)