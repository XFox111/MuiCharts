FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /app

COPY ./MuiCharts.sln ./
COPY ./MuiCharts.Domain/*.csproj MuiCharts.Domain/
COPY ./MuiCharts.Contracts/*.csproj MuiCharts.Contracts/
COPY ./MuiCharts.Infrastructure/*.csproj MuiCharts.Infrastructure/
COPY ./MuiCharts.Api/*.csproj MuiCharts.Api/

RUN dotnet restore

COPY . ./

RUN dotnet publish --configuration Release -o out MuiCharts.Api/MuiCharts.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime

WORKDIR /app

COPY --from=build /app/out ./
RUN mkdir /persistence

ENV HTTP_PORTS=80
EXPOSE 80

ENTRYPOINT ["dotnet", "MuiCharts.Api.dll"]
