# 1. CONSTRUCCIÓN (Usamos el SDK 9.0)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# COPIAR TODO: Copiamos todo el repositorio sin filtrar
COPY . .

# NAVEGAR: Entramos a la carpeta del proyecto
WORKDIR /src/TripsMicroservice

# RESTAURAR: Ahora sí funcionará porque tenemos el SDK correcto
RUN dotnet restore

# PUBLICAR
RUN dotnet publish -c Release -o /app/publish

# 2. EJECUCIÓN (Usamos el Runtime 9.0)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TripsMicroservice.dll"]