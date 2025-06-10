# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "LeverXGameCollectorProject/LeverXGameCollectorProject.API.csproj"
RUN dotnet publish "LeverXGameCollectorProject/LeverXGameCollectorProject.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LeverXGameCollectorProject.API.dll"]