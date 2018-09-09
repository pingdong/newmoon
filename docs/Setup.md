[Back](../README.md)

## Setup 

## Requirement:
1. Visual Studio 2017
2. Asp.Net Core 2.1.2 (If your version of VS doesn't include it) [Download here](https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.1.2-download.md)
3. Docker 18.06-CE

## Database
A database is created and granted proper permission.

## Update Setting

 The following settings need to be changed accroding your own settings in .env file

```  
  SqlServer_ConnectionString=Server=192.168.5.1,1433;Database=[your db name];User Id=[user id];Password=[pwd];MultipleActiveResultSets=true
```

