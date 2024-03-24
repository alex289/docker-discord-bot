using Discord.WebSocket;

namespace DockerDiscordBot.Commands.PullImage;

public sealed class PullImageCommand : Command
{
    public PullImageCommand(SocketMessage message) : base(message)
    {
    }
}