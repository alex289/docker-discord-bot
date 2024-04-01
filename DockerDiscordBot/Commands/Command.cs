using Discord.WebSocket;
using FluentValidation.Results;
using MediatR;

namespace DockerDiscordBot.Commands;

public abstract class Command : IRequest
{
    public SocketMessage Message { get; }
    public ValidationResult? ValidationResult { get; protected set; }

    protected Command(SocketMessage message)
    {
        Message = message;
    }

    public abstract bool IsValid();
}