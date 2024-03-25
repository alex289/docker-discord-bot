using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands;

public abstract class CommandHandler<T> : IRequestHandler<T> where T : Command
{
    protected readonly ILogger<CommandHandler<T>> Logger;

    protected CommandHandler(ILogger<CommandHandler<T>> logger)
    {
        Logger = logger;
    }

    public abstract Task Handle(T request, CancellationToken cancellationToken);
}