#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["2 - Workers/ScheduleWorker/ScheduleWorker.csproj", "2 - Workers/ScheduleWorker/"]
COPY ["3 - Application/Application/Application.csproj", "3 - Application/Application/"]
COPY ["4 - Domain/Domain/Domain.csproj", "4 - Domain/Domain/"]
COPY ["5 - Infra/Infra/Infra.csproj", "5 - Infra/Infra/"]
COPY ["6 - CrossCutting/CrossCutting/CrossCutting.csproj", "6 - CrossCutting/CrossCutting/"]
RUN dotnet restore "./2 - Workers/ScheduleWorker/ScheduleWorker.csproj"
COPY . .
WORKDIR "/src/2 - Workers/ScheduleWorker"
RUN dotnet build "./ScheduleWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ScheduleWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduleWorker.dll"]