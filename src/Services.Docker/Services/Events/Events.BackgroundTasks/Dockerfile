FROM microsoft/dotnet:2.2.1-aspnetcore-runtime-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2.103-sdk-nanoserver-1809 AS build
WORKDIR /src
COPY Services/Events/Events.BackgroundTasks/Events.BackgroundTasks.csproj Services/Events/Events.BackgroundTasks/
COPY Library/Shared/Shared.csproj Library/Shared/
COPY Library/EventBus/EventBus/EventBus.csproj Library/EventBus/EventBus/
COPY Library/EntityFrameworkCore/EntityFrameworkCore.csproj Library/EntityFrameworkCore/
COPY Library/Web/Web.csproj Library/Web/
COPY Library/DomainDriven/Service/DomainDriven.Service.csproj Library/DomainDriven/Service/
COPY Library/DomainDriven/Core/DomainDriven.Core.csproj Library/DomainDriven/Core/
COPY Library/DomainDriven/Infrastructure/DomainDriven.Infrastructure.csproj Library/DomainDriven/Infrastructure/
RUN dotnet restore Services/Events/Events.BackgroundTasks/Events.BackgroundTasks.csproj
COPY . .
WORKDIR /src/Services/Events/Events.BackgroundTasks
RUN dotnet build Events.BackgroundTasks.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Events.BackgroundTasks.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PingDong.Newmoon.Events.BackgroundTasks.dll"]
