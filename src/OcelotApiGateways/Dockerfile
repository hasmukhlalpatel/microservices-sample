FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["OcelotApiGateways/OcelotApiGateways.csproj", "OcelotApiGateways/"]
RUN dotnet restore "OcelotApiGateways/OcelotApiGateways.csproj"
COPY . .
WORKDIR "/src/OcelotApiGateways"
RUN dotnet build "OcelotApiGateways.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OcelotApiGateways.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OcelotApiGateways.dll"]