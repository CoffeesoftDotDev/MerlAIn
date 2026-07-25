# syntax=docker/dockerfile:1
# Build context is the repository root so the shared library is available.

FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

COPY Directory.Build.props ./
COPY libs/dotnet/MerlAIn.Shared/MerlAIn.Shared.csproj libs/dotnet/MerlAIn.Shared/
COPY apps/agents/MerlAIn.Agents.csproj apps/agents/
RUN dotnet restore apps/agents/MerlAIn.Agents.csproj

COPY libs/dotnet/ libs/dotnet/
COPY apps/agents/ apps/agents/
RUN dotnet publish apps/agents/MerlAIn.Agents.csproj \
      --configuration Release \
      --no-restore \
      --output /app

# The shared library carries a Microsoft.AspNetCore.App framework reference, so the worker runs on
# the ASP.NET Core runtime image even though it serves no HTTP traffic.
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app

ENV DOTNET_EnableDiagnostics=0

COPY --from=build /app ./

USER $APP_UID

ENTRYPOINT ["dotnet", "MerlAIn.Agents.dll"]
