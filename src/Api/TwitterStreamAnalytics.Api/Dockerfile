#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TwitterStreamAnalytics.Api/TwitterStreamAnalytics.Api.csproj", "TwitterStreamAnalytics.Api/"]
RUN dotnet restore "TwitterStreamAnalytics.Api/TwitterStreamAnalytics.Api.csproj"
COPY . .
WORKDIR "/src/TwitterStreamAnalytics.Api"
RUN dotnet build "TwitterStreamAnalytics.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TwitterStreamAnalytics.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TwitterStreamAnalytics.Api.dll"]