using Discord.WebSocket;

namespace DockerDiscordBot.Commands.RemoveImage;

public sealed class RemoveImageCommand : Command
{
    public RemoveImageCommand(SocketMessage message) : base(message)
    {
    }
}