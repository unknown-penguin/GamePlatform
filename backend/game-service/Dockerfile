# Use the official .NET image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5031
ENV ASPNETCORE_URLS=http://+:5031   

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY backend/game-service/game-service.csproj backend/game-service/
RUN dotnet restore backend/game-service/game-service.csproj
COPY backend/game-service/ backend/game-service/
WORKDIR /src/backend/game-service
RUN dotnet build game-service.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish game-service.csproj -c Release -o /app/publish

# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "game-service.dll"]