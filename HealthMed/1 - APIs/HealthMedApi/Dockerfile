#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8085

ENV ASPNETCORE_URLS=http://+:8005;http://+:8085;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["1 - APIs/HealthMedApi/HealthMedApi.csproj", "1 - APIs/HealthMedApi/"]
COPY ["3 - Application/Application/Application.csproj", "3 - Application/Application/"]
COPY ["4 - Domain/Domain/Domain.csproj", "4 - Domain/Domain/"]
COPY ["5 - Infra/Infra/Infra.csproj", "5 - Infra/Infra/"]
COPY ["6 - CrossCutting/CrossCutting/CrossCutting.csproj", "6 - CrossCutting/CrossCutting/"]
RUN dotnet restore "./1 - APIs/HealthMedApi/HealthMedApi.csproj"
COPY . .
WORKDIR "/src/1 - APIs/HealthMedApi"
RUN dotnet build "./HealthMedApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HealthMedApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthMedApi.dll"]