[Back](../README.md)

> ## Background Tasks 


In some case a background service may need run along with WebApp, there are a few ways to implement it.

1. IHostService  

The way you add one or multiple IHostedServices into your WebHost or Host is by registering them up through the standard DI in an ASP.NET Core WebHost (or in a Host in .NET Core 2.1). Basically, you have to register the hosted services within the familiar ConfigureServices() method of the Startup class, as in the following code from a typical ASP.NET WebHost. 

When the service is registered, it will be initialized when startup is finished. 

Schedule setting is in cron express. [Cron Express](https://en.wikipedia.org/wiki/Cron)

2. Azure Functions

If the WebApp is hosting in Azure, Azure Functions is a better choice for this scenario, except a cost involved.

