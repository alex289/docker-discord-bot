# Docker Discord Bot

[![license](https://badgen.net/github/license/alex289/docker-discord-bot)](https://github.com/alex289/docker-discord-bot/blob/main/LICENSE)

An easy-to-use Discord bot to manage your Docker containers.

## Features

-  Ping the bot to check if it's online
-  Create and remove Docker containers
-  Start, stop, and restart Docker containers
-  List all running Docker containers
-  Get detailed information about a specific Docker container
-  Get detailed information about the host system

## Installation 🚀

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

## License

© [Alexander Konietzko](https://alexanderkonietzko.com) 2024  
Released under the [MIT license](https://github.com/alex289/docker-discord-bot/blob/main/LICENSE)
