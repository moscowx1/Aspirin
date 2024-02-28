FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src
COPY Identity.csproj .
RUN dotnet restore
COPY . .
ARG configuration
RUN dotnet publish Identity.csproj -c $configuration -o /app
WORKDIR /app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Identity.dll"]