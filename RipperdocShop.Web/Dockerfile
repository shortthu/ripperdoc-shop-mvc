﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RipperdocShop.Web/RipperdocShop.Web.csproj", "RipperdocShop.Web/"]
RUN dotnet restore "RipperdocShop.Web/RipperdocShop.Web.csproj"
COPY . .
WORKDIR "/src/RipperdocShop.Web"
RUN dotnet build "./RipperdocShop.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RipperdocShop.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RipperdocShop.Web.dll"]
