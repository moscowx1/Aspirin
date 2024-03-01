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
ARG CON_STRING_ID
ENV ConnectionStrings:Identity=${CON_STRING_ID}
ARG CONFIGURATION
ENV ASPNETCORE_ENVIRONMENT=${CONFIGURATION}
ARG OTLP_ENDPOINT
ENV ConnectionStrings:OltpEndpoint=${OTLP_ENDPOINT}
WORKDIR /app
COPY --from=publish /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Identity.dll"]