using Discord.WebSocket;

namespace DockerDiscordBot.Commands.ShowContainer;

public sealed class ShowContainerCommand : Command
{
    public ShowContainerCommand(SocketMessage message) : base(message)
    {
    }
}