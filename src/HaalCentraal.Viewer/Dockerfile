#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
COPY certificate/api.haalcentraal.local.crt /usr/local/share/ca-certificates/api.haalcentraal.local.crt
COPY certificate/viewer.haalcentraal.local.crt /usr/local/share/ca-certificates/viewer.haalcentraal.local.crt
COPY certificate/oidc.haalcentraal.local.crt /usr/local/share/ca-certificates/oidc.haalcentraal.local.crt
RUN update-ca-certificates

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/HaalCentraal.Viewer/HaalCentraal.Viewer.csproj", "src/HaalCentraal.Viewer/"]
RUN dotnet restore "src/HaalCentraal.Viewer/HaalCentraal.Viewer.csproj"
COPY . .
WORKDIR "/src/src/HaalCentraal.Viewer"
RUN dotnet build "HaalCentraal.Viewer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HaalCentraal.Viewer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HaalCentraal.Viewer.dll"]
