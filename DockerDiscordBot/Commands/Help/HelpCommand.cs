using Discord.WebSocket;

namespace DockerDiscordBot.Commands.Help;

public sealed class HelpCommand : Command
{
    public HelpCommand(SocketMessage message) : base(message)
    {
    }
    
    public override bool IsValid() => true;
}