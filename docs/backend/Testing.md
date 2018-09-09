[Back](../dotnet-Backend.md)

> ## Testing 

## Prerequirements
	
SQL Server Express need to be installed for Integration and Functional Testing. 

## How to run

Run directly from Test Exploer

Or

Run `dotnet test` from the direction that *.proj file exists
	
	
## Clean Up

In Integration / Funcations testing, a pair of mdf/ldf with GUID value as its filename are created under `C:\Users\[Username]`. Those files should be deleted after running. 

But if the test case is interrupted, the clean up process doesn't have chance to be executed. Those files can be manually deleted.

If test cases are run in parallel, multiple pairs of database files could be found.


## Two approaches:

There are two approaches of implementing Integration and Functional testing. One approach uses `WebApplicationFactory<TStartUp>`, while the other approach create TestServer on my own. 

WebApplicationFactory is the way recommended in Asp.Net Core [docs](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.1)


I personal like the second approach. There are a few reasons
1. The second approach is straight forward, whereas the first is a blackbox.
2. In WebApplicationFactory, the whole test class likely shares the same database across all test cases in the same test class. Any individual test case potentially pollutes the whole test environment, the later test cases may fail by previous testings. Of course, there are some workarounds. But all workarounds increase complexity unnecessarily. 
3. Sharing the same database means running tests in parallel doesn't support, manual isolation is needed. Again, unnecessarily.


## Notes:

1. Demo purpose only, therefore just a few sample tests are provided.
2. Integration / Functional Testing  
	* All tests can have its own separate setting, settings.json exists in each test case folder. Events.Api.FunctionalTest is implemented in this approach.  
	* All tests can have a shared setting, settings.json exists under project level. Events.Api.IntegrationTest follows this way.  
	* Shared and dedicated settings could be mixed in the same test project, or even shared across multiple projects.  
	* Every test case recreate database and remove it after testing, so every test won't pollute each other. An extra benefit is data clean up process is not needed any more.   
	* As every test has to setup the whole web host stack, database, an individual test expectedly runs longer.  
