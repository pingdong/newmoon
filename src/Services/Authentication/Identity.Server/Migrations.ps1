# Need to be run under folder that csproj exists

dotnet ef migrations remove --context ConfigurationDbContext
dotnet ef migrations add Identity.Configuration -c ConfigurationDbContext -o ./IdentityServer/Migrations/Configuration
 
dotnet ef migrations remove --context PersistedGrantDbContext
dotnet ef migrations add Identity.PersistedGrant -c PersistedGrantDbContext -o ./IdentityServer/Migrations/PersistedGrant