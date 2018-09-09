[Back](../dotnet-backend.md)

## Dependency Injection 

In Newmoon, classes are registed in `AutofacModuleRegistrar.cs` or `Registrar.cs`. All registrars are discovered and registered automatically, by Reflection under the hood.

This approach honors SoC pattern, instead of manually registering in single place.

What files are registered is defined in method `GetSearchingTargets()` in `startup.cs` in `events.api.csproj`,  

Some useful __Autofac__ information here  

[Integration with Asp.Net Core](http://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html)  
[Best practices](http://autofaccn.readthedocs.io/en/latest/best-practices/index.html)  