FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ASP NET API +Angular 2.csproj", "./"]
RUN dotnet restore "ASP NET API +Angular 2.csproj"
COPY . .
WORKDIR "/src/."
FROM build AS publish
RUN dotnet publish "ASP NET API +Angular 2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASP NET API +Angular 2.dll"]
