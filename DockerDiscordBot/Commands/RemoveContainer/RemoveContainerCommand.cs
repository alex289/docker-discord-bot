using Discord.WebSocket;

namespace DockerDiscordBot.Commands.RemoveContainer;

public sealed class RemoveContainerCommand : Command
{
    public RemoveContainerCommand(SocketMessage message) : base(message)
    {
    }
}