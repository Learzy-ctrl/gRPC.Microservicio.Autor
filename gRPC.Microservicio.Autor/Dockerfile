# Usar la imagen oficial de .NET 7 SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copiar los archivos csproj y restaurar las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos y compilar la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Usar la imagen oficial de .NET 7 Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Exponer el puerto gRPC
EXPOSE 5000

# Definir el comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "gRPC.Microservicio.Autor.dll"]
