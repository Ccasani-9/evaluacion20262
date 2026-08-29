FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080
# Evita crashes (SIGSEGV) del runtime .NET en el sandbox de contenedores de Render.
ENV DOTNET_EnableWriteXorExecute=0
# Desactiva el FileSystemWatcher de configuración: el sandbox de Render limita mucho
# los inotify y esa vigilancia de archivos tumbaba la app al iniciar.
ENV DOTNET_hostBuilder__reloadConfigOnChange=false
EXPOSE 8080

ENTRYPOINT ["dotnet", "TecnoGasHogar.dll"]
