FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DockerDiscordBot/DockerDiscordBot.csproj", "DockerDiscordBot/"]
RUN dotnet restore "DockerDiscordBot/DockerDiscordBot.csproj"
COPY . .
WORKDIR "/src/DockerDiscordBot"
RUN dotnet build "DockerDiscordBot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DockerDiscordBot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
LABEL org.opencontainers.image.authors="Alexander Konietzko"
LABEL org.opencontainers.image.title="Docker Discord Bot"
LABEL org.opencontainers.image.description="An easy-to-use Discord bot to manage your Docker containers"
ENTRYPOINT ["dotnet", "DockerDiscordBot.dll"]
