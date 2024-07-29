#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Global.ProductsManagement.Api/Global.ProductsManagement.Api.csproj", "Global.ProductsManagement.Api/"]
COPY ["Global.ProductsManagement.Application/Global.ProductsManagement.Application.csproj", "Global.ProductsManagement.Application/"]
COPY ["Global.ProductsManagement.Domain/Global.ProductsManagement.Domain.csproj", "Global.ProductsManagement.Domain/"]
COPY ["Global.ProductsManagement.Infraestructure/Global.ProductsManagement.Infraestructure.csproj", "Global.ProductsManagement.Infraestructure/"]
RUN dotnet restore "./Global.ProductsManagement.Api/Global.ProductsManagement.Api.csproj"
COPY . .
WORKDIR "/src/Global.ProductsManagement.Api"
RUN dotnet build "./Global.ProductsManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Global.ProductsManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Global.ProductsManagement.Api.dll"]