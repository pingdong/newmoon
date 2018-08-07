[Back](../README.md)

> ## Setup 

## Requirement:
1. Visual Studio 2017
2. Asp.Net Core 2.1.2 (If your version of VS doesn't include it) [Download here](https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.1.2-download.md)

## Note:

 The following settings need to be included in appsettings.Development.json or user secrects in Identity.Server

```  
  "IdentityServiceUri": "http://localhost:5001",
  "EventsServiceUri": "http://localhost:5005",
  "DbConnectionString": "Server=;Database=;User Id=;Password=;MultipleActiveResultSets=true"
```

