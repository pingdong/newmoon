FROM microsoft/dotnet:2.1.3-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1.302-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY Services/Events/Events.API/Events.Api.csproj Services/Events/Events.API/
COPY Services/Events/Core/Events.Core.csproj Services/Events/Core/
COPY Library/DomainDriven/Core/DomainDriven.Core.csproj Library/DomainDriven/Core/
COPY Library/Shared/Shared.csproj Library/Shared/
COPY Library/EventBus/EventBus/EventBus.csproj Library/EventBus/EventBus/
COPY Library/EventBus/EventBus.AspNetCore/EventBus.AspNetCore.csproj Library/EventBus/EventBus.AspNetCore/
COPY Library/EntityFrameworkCore/EntityFrameworkCore.csproj Library/EntityFrameworkCore/
COPY Services/Events/Service/Events.Service.csproj Services/Events/Service/
COPY Services/Events/Infrastructure/Events.Infrastructure.csproj Services/Events/Infrastructure/
COPY Library/DomainDriven/Infrastructure/DomainDriven.Infrastructure.csproj Library/DomainDriven/Infrastructure/
COPY Library/DomainDriven/Service/DomainDriven.Service.csproj Library/DomainDriven/Service/
COPY Library/Web/Web.csproj Library/Web/
RUN dotnet restore Services/Events/Events.API/Events.Api.csproj
COPY . .
WORKDIR /src/Services/Events/Events.API
RUN dotnet build Events.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Events.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PingDong.Newmoon.Events.dll"]
