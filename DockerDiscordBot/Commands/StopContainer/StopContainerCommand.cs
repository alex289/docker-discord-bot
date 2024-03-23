using Discord.WebSocket;

namespace DockerDiscordBot.Commands.StopContainer;

public sealed class StopContainerCommand : Command
{
    public StopContainerCommand(SocketMessage message) : base(message)
    {
    }
}