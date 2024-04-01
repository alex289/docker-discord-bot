using Discord.WebSocket;

namespace DockerDiscordBot.Commands.Ping;

public sealed class PingCommand : Command
{
    public PingCommand(SocketMessage message) : base(message)
    {
    }

    public override bool IsValid()
    {
        return true;
    }
}