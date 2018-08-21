# Need to be run under folder that csproj exists

dotnet ef migrations remove --context ConfigurationDbContext
dotnet ef migrations add Identity.Configuration -c ConfigurationDbContext -o ./Infrastructure/IdentityServer/Migrations/Configuration
 
dotnet ef migrations remove --context PersistedGrantDbContext
dotnet ef migrations add Identity.PersistedGrant -c PersistedGrantDbContext -o ./Infrastructure/IdentityServer/Migrations/PersistedGrant