# Use the official .NET image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5049
ENV ASPNETCORE_URLS=http://+:5049   

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY backend/game-room-service/game-room-service.csproj backend/game-room-service/
RUN dotnet restore backend/game-room-service/game-room-service.csproj
COPY backend/game-room-service/ backend/game-room-service/
WORKDIR /src/backend/game-room-service
RUN dotnet build game-room-service.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish game-room-service.csproj -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "game-room-service.dll"]