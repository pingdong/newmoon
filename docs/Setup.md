[Back](../README.md)

> ## Setup 

## Requirement:
1. Visual Studio 2017
2. Asp.Net Core 2.1.2 (If your version of VS doesn't include it) [Download here](https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.1.2-download.md)

## Note:

 1. The following settings need to be provided, prefer in user secrects in Events.Api projects

```
  "ConnectionStrings": {  
	DefaultDbConnection": "Server=[server name];Database=[database name];User Id=[username];Password=[password];MultipleActiveResultSets=true"
  },
  "Redis": {
	"Connection":  "" 
  },
  "DistributedCache": {
	"Server": "",
	"Instance":  "" 
  } 
```


 2. The following settings need to be included in appsettings.Development.json or user secrects in Identity.Server

```
  "ConnectionStrings": {
	"DefaultDbConnection": "Server=[server name];Database=[database name];User Id=[username];Password=[password];MultipleActiveResultSets=true"
  }
```
