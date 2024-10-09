FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY customers.sln .
COPY src/Api/Customers.API/Customers.API.csproj src/Customers.API/
COPY src/Api/Customers.Domain/Customers.Domain.csproj src/Customers.Domain/
COPY src/Api/Customers.Infrastructure/Customers.Infrastructure.csproj src/Customers.Infrastructure/

COPY Directory.Build.props .
RUN dotnet restore 

COPY . .
FROM build AS publish
RUN dotnet publish src/Customers.API/Customers.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.API.dll"]