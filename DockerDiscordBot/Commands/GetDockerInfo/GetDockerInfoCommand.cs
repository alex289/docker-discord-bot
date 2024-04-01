using Discord.WebSocket;

namespace DockerDiscordBot.Commands.GetDockerInfo;

public sealed class GetDockerInfoCommand : Command
{
    public GetDockerInfoCommand(SocketMessage message) : base(message)
    {
    }

    public override bool IsValid()
    {
        return true;
    }
}