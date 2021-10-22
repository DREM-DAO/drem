#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["market/api/DREM-API/", "./"]
RUN dotnet restore "DREM-API/DREM-API.csproj"
COPY . .
WORKDIR "/src/DREM-API"
RUN dotnet build "DREM-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DREM-API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DREM-API.dll"]