FROM microsoft/dotnet:2.1.3-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1.302-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY Services/Authentication/Identity.Server/Identity.Server.csproj Services/Authentication/Identity.Server/
COPY Library/Shared/Shared.csproj Library/Shared/
COPY Library/Web/Web.csproj Library/Web/
COPY Library/DomainDriven/Service/DomainDriven.Service.csproj Library/DomainDriven/Service/
COPY Library/DomainDriven/Core/DomainDriven.Core.csproj Library/DomainDriven/Core/
COPY Library/DomainDriven/Infrastructure/DomainDriven.Infrastructure.csproj Library/DomainDriven/Infrastructure/
RUN dotnet restore Services/Authentication/Identity.Server/Identity.Server.csproj
COPY . .
WORKDIR /src/Services/Authentication/Identity.Server
RUN dotnet build Identity.Server.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Identity.Server.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PingDong.Newmoon.IdentityServer.dll"]
