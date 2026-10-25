FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY src/Homolog.Fixture/Homolog.Fixture.csproj src/Homolog.Fixture/
RUN dotnet restore src/Homolog.Fixture/Homolog.Fixture.csproj
COPY src/Homolog.Fixture/ src/Homolog.Fixture/
RUN dotnet publish src/Homolog.Fixture/Homolog.Fixture.csproj -c Release --no-restore -o /out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out/ .
USER $APP_UID
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Homolog.Fixture.dll"]
