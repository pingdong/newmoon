[Back](../dotnet-Backend.md)

> ## Data Validation 

Data Validation happens in 3 places

1. In Asp.Net Core pipeline
	* __ModelState__ is validated by `ModelStateValidationFilter`, installed in mvc filters, in calling AddMvc or AddMvcCore. This filter applies globally and automatically, instead of valdating ModelState in every method in controllers.
	* __DTO__ or __Command__ is validated by FluentValidation. All validations are added by `AddFluentValidation` in calling AddMvc or AddMvcCore

2. In MediatR behaviour pipeline
	In CQRS pattern, if data is validated by FluentValidation in MVC, validation behaviours in MediatR pipeline is redundant, and prefer done in Asp.Net Core pipeline.    

3. Before persisting data 
	Data should be validate as DTO are very likely translated, reshaped into a new data for data persistent.

> ## FluentValidation 8.0 Changelog


[Here](https://fluentvalidation.net/upgrading-to-8.html)