# syntax=docker/dockerfile:1
# Build context is the repository root so the shared library is available.

FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# Restore on project files only: this layer is cached until a dependency actually changes.
COPY Directory.Build.props ./
COPY libs/dotnet/MerlAIn.Shared/MerlAIn.Shared.csproj libs/dotnet/MerlAIn.Shared/
COPY apps/api/MerlAIn.Api.csproj apps/api/
RUN dotnet restore apps/api/MerlAIn.Api.csproj

COPY libs/dotnet/ libs/dotnet/
COPY apps/api/ apps/api/
RUN dotnet publish apps/api/MerlAIn.Api.csproj \
      --configuration Release \
      --no-restore \
      --output /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080 \
    DOTNET_EnableDiagnostics=0

COPY --from=build /app ./

# APP_UID is the non-root user provided by the official .NET images.
USER $APP_UID
EXPOSE 8080

ENTRYPOINT ["dotnet", "MerlAIn.Api.dll"]
