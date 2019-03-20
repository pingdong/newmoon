[Back](../dotnet_core.md)

## EF Core 

## Migration

More Migration information could be found [here](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)

__Precondition__
* Only support .Net Core or .Net Framework project, .Net Standard project is not supported  
* Implemented IDesignTimeDbContextFactory<>

__Commands__

Create  
	`dotnet ef migrations add InitialCreate`
		
Update  
	`dotnet ef database update`

Remove  
	`dotnet ef migration remove`

Revert  
	`dotnet ef database update LastGoodMigration`

Generating Scripts  
	`dotnet ef migrations script`


## Transaction

Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction(): [here]( https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency)