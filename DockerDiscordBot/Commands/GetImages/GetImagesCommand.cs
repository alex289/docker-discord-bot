using Discord.WebSocket;

namespace DockerDiscordBot.Commands.GetImages;

public sealed class GetImagesCommand : Command
{
    public GetImagesCommand(SocketMessage message) : base(message)
    {
    }
}