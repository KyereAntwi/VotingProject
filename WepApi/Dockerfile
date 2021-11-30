#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WepApi/WepApi.csproj", "WepApi/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["DTOs/DTOs.csproj", "DTOs/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
RUN dotnet restore "WepApi/WepApi.csproj"
COPY . .
WORKDIR "/src/WepApi"
RUN dotnet build "WepApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WepApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WepApi.dll"]