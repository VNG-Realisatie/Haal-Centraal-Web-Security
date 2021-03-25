#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/HaalCentraal.IdentityServer/HaalCentraal.IdentityServer.csproj", "src/HaalCentraal.IdentityServer/"]
RUN dotnet restore "src/HaalCentraal.IdentityServer/HaalCentraal.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/HaalCentraal.IdentityServer"
RUN dotnet build "HaalCentraal.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HaalCentraal.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HaalCentraal.IdentityServer.dll"]