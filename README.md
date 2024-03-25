# Docker Discord Bot

![GitHub License](https://img.shields.io/github/license/alex289/docker-discord-bot)
![Docker Pulls](https://img.shields.io/docker/pulls/alexdev28/docker-discord-bot)
[![CodeFactor](https://www.codefactor.io/repository/github/alex289/docker-discord-bot/badge)](https://www.codefactor.io/repository/github/alex289/docker-discord-bot)
![Docker Image Size](https://img.shields.io/docker/image-size/alexdev28/docker-discord-bot)

An easy-to-use Discord bot to manage your Docker containers.

## Table of Contents

- [Commands](#commands)
- [Installation](#installation)
  - [Discord Bot Setup](#discord-bot-setup)
  - [Docker Setup](#docker-setup)
  - [Configuration](#configuration)
- [License](#license)

## Commands

| Command | Description |
| --- | --- |
| `!help` | Show all available commands |
| `!ping` | Check if the bot is online |
| `!dockerps` | List all running Docker containers |
| `!dockerstop <container>` | Stop a Docker container |
| `!dockerstart <container>` | Start a Docker container |
| `!dockerrestart <container>` | Restart a Docker container |
| `!dockerremove <container>` | Remove a Docker container |
| `!dockershow <container>` | Show information about a Docker container |
| `!dockercreate <image>:<tag>/<image> <name> <ports (80:80,81:81)>` | Create a new Docker container |
| `!dockerinfo` | Show information about the Docker host |
| `!dockerimages` | List all existing images |
| `!dockerpull <image>:<tag>/<image>` | Pull an image |
| `!dockerremoveimage <image>` | Remove an image |

## Installation

### Discord Bot Setup

1.  Create a new Discord bot at the [Discord Developer Portal](https://discord.com/developers/applications)
2.  Copy the bot token
3.  Invite the bot to your server using the following link: `https://discord.com/oauth2/authorize?client_id=YOUR_BOT_ID&permissions=8&scope=bot`

### Docker Setup

Run the following command to start the Docker container:

```bash
docker run \
    -d \
    --name docker-discord-bot \
    -u root \
    -e ApplicationSettings__DiscordToken=YOUR_DISCORD_BOT_TOKEN \
    -v /var/run/docker.sock:/var/run/docker.sock \
    alexdev28/docker-discord-bot:latest
```

Or with docker compose:

```yaml
version: '3.8'

services:
  docker-discord-bot:
    image: alexdev28/docker-discord-bot:latest
    container_name: docker-discord-bot
    restart: unless-stopped
    user: root
    environment:
      - ApplicationSettings__DiscordToken=YOUR_DISCORD_BOT_TOKEN
    volumes:
      - /var/run/docker.sock:/var/run/docker.sock
```

### Configuration

The bot can be configured using environment variables:

| Environment Variable | Description | Default Value | Required |
| --- | --- | --- | --- |
| `ApplicationSettings__DiscordToken` | Discord bot token | `null` | ✅ |
| `ApplicationSettings__AdminUser` | Discord admin username | `null` | ❌ |
| `ApplicationSettings__CommandPrefix` | Command prefix | `!` | ❌ |
| `ApplicationSettings__DockerRegistryUrl` | Private registry url | `null` | ❌ |
| `ApplicationSettings__DockerRegistryUsername` | Private registry username | `null` | ❌ |
| `ApplicationSettings__DockerRegistryPassword` | Private registry password | `null` | ❌ |

## License

© [Alexander Konietzko](https://alexanderkonietzko.com) 2024  
Released under the [MIT license](https://github.com/alex289/docker-discord-bot/blob/main/LICENSE)
