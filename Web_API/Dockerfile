# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM alpine:latest
# RUN apk add --upgrade ca-certificates

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
# ENV ASPNETCORE_ENVIRONMENT=Development # This is important to use swagger inside docker
EXPOSE 8080
EXPOSE 443

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web_API/Web_API.csproj", "Web_API/"]
COPY ["Repositories/Services.csproj", "Repositories/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["BusinessObjects/BusinessObjects.csproj", "BusinessObjects/"]
RUN dotnet restore "./Web_API/Web_API.csproj"
COPY . .
WORKDIR "/src/Web_API"
RUN dotnet build "./Web_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
RUN dotnet publish "./Web_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy SSL certificates
COPY ["aspnet.crt", "./"]
COPY ["aspnet.key", "./"]

# Set ASP.NET Core to use Kestrel server with HTTPS
ENV ASPNETCORE_URLS="http://+:80;https://+:443"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="./aspnet.crt"
ENV ASPNETCORE_Kestrel__Certificates__Default__KeyPath="./aspnet.key"

ENTRYPOINT ["dotnet", "Web_API.dll"]
