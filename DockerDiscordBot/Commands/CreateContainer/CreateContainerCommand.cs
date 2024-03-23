using Discord.WebSocket;

namespace DockerDiscordBot.Commands.CreateContainer;

public sealed class CreateContainerCommand : Command
{
    public CreateContainerCommand(SocketMessage message) : base(message)
    {
    }
}