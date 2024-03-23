using Discord.WebSocket;

namespace DockerDiscordBot.Commands.RestartContainer;

public sealed class RestartContainerCommand : Command
{
    public RestartContainerCommand(SocketMessage message) : base(message)
    {
    }
}