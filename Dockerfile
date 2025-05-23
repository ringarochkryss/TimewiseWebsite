# Build stage
FROM mcr.microsoft.com / dotnet / sdk:7.0 AS build

WORKDIR /app

# Kopiera projektfil och återställ dependencies
COPY *.csproj ./
RUN dotnet restore

# Kopiera all kod och bygg projektet
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

WORKDIR /app

# Kopiera publicerad app från build-steget
COPY --from=build /app/out ./

# Sätt miljövariabel så appen lyssnar på rätt port (Heroku sätter $PORT)
ENV ASPNETCORE_URLS=http://+:${PORT:-80}

# Starta appen
ENTRYPOINT["dotnet", "Salto.dll"]
