FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["PozorDomAuthService.slnx", "."]

COPY ["PozorDomAuthService.Api/PozorDomAuthService.Api.csproj", "PozorDomAuthService.Api/"]
COPY ["PozorDomAuthService.Application/PozorDomAuthService.Application.csproj", "PozorDomAuthService.Application/"]
COPY ["PozorDomAuthService.Domain/PozorDomAuthService.Domain.csproj", "PozorDomAuthService.Domain/"]
COPY ["PozorDomAuthService.Infrastructure/PozorDomAuthService.Infrastructure.csproj", "PozorDomAuthService.Infrastructure/"]
COPY ["PozorDomAuthService.Persistence/PozorDomAuthService.Persistence.csproj", "PozorDomAuthService.Persistence/"]

RUN dotnet restore "PozorDomAuthService.slnx"

COPY . .

WORKDIR "/src/PozorDomAuthService.Api"
RUN dotnet build "../PozorDomAuthService.slnx" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PozorDomAuthService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PozorDomAuthService.Api.dll"]
