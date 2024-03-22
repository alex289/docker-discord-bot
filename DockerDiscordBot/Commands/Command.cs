using Discord.WebSocket;
using MediatR;

namespace DockerDiscordBot.Commands;

public abstract class Command : IRequest
{
    public SocketMessage Message { get; }

    protected Command(SocketMessage message)
    {
        Message = message;
    }
}