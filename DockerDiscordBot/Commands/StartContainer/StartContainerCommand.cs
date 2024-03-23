using Discord.WebSocket;

namespace DockerDiscordBot.Commands.StartContainer;

public sealed class StartContainerCommand : Command
{
    public StartContainerCommand(SocketMessage message) : base(message)
    {
    }
}