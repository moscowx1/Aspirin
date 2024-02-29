ARG DOTNET_SDK
ARG DOTNET_RUNTIME
FROM ${DOTNET_SDK} AS publish
WORKDIR /src
COPY Identity.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish Identity.csproj -o /app
WORKDIR /app
FROM ${DOTNET_RUNTIME} AS base
ARG CONFIGURATION
ARG CON_STRING_ID
ENV ConnectionStrings:Identity=${CON_STRING_ID}
ENV ASPNETCORE_ENVIRONMENT=${CONFIGURATION}
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Identity.dll"]