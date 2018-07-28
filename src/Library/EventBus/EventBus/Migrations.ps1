# Need to be run under folder that csproj exists

dotnet ef migrations remove --context EventBusLogServiceDbContext
dotnet ef migrations add EventBusLogService -c EventBusLogServiceDbContext -o ./Service/Migrations