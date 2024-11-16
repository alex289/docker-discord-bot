FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
WORKDIR /src
COPY ["DockerDiscordBot/DockerDiscordBot.csproj", "DockerDiscordBot/"]
RUN dotnet restore "DockerDiscordBot/DockerDiscordBot.csproj" -a $TARGETARCH
COPY . .
WORKDIR "/src/DockerDiscordBot"
RUN dotnet build "DockerDiscordBot.csproj" -c $BUILD_CONFIGURATION -o /app/build -a $TARGETARCH

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
RUN dotnet publish "DockerDiscordBot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore -a $TARGETARCH

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
LABEL org.opencontainers.image.authors="Alexander Konietzko"
LABEL org.opencontainers.image.title="Docker Discord Bot"
LABEL org.opencontainers.image.description="An easy-to-use Discord bot to manage your Docker containers"
LABEL org.opencontainers.image.source = "https://github.com/alex289/docker-discord-bot"
ENTRYPOINT ["dotnet", "DockerDiscordBot.dll"]
