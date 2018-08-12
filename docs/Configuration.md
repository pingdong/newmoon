[Back](../README.md)

> ## Configuration 

## Sources

There are different kinds of configurations in one application, likely in a multi-level structure.


1. Settings need to be secured, like connection string, user credential, and they are very unlikely same in differnt environment. Those sensitive settings will keep in `User Secrets` for develpment, while in `Vault` in Production.  
	* DEV:  User secrets  
	* PROD: Vault
	
2. Settings are differnt in Development and Production. In development, they are set by `Environment Variables` or `Command line arguments`, whereas through `Environment Variables` in Production
	* DEV:  Environment Variables / Command line arguments
	* PROD: Environment Variables

3. Application specific settings, such as Log setting. Normally, those settings are stored in json files. If some settings need to be changed in development, a development version setting can be used to only have those settings.  
	* DEV:  appsettings.development.json
	* PROD: appsettings.json`


## Override

If the same setting is provided in all places listed above, up level of the setting overrides below level.

`Environment Variables / Commandline arguments` 

overrides 

`Vault / User Secrets` 

overrides

`appsettings.development.json`

overrides

`appsettings.json`

## Notes

In testing, in-memory setting provider is used to provide dynamic context awared settings. 