using Discord.WebSocket;

namespace DockerDiscordBot.Commands.GetContainers;

public sealed class GetContainersCommand : Command
{
    public GetContainersCommand(SocketMessage message) : base(message)
    {
    }
    
    public override bool IsValid() => true;
}