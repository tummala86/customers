FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Customers.WebApp/Customers.WebApp.csproj", "Customers.WebApp/"]
RUN dotnet restore "Customers.WebApp/Customers.WebApp.csproj"
COPY . .
WORKDIR "/src/Customers.WebApp"
RUN dotnet build "Customers.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customers.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.WebApp.dll"]