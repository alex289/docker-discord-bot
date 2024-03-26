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
    
    protected async ValueTask<bool> TestValidityAsync(Command command)
    {
        if (command.IsValid())
        {
            return true;
        }

        if (command.ValidationResult is null)
        {
            throw new InvalidOperationException("Command is invalid and should therefore have a validation result");
        }

        foreach (var error in command.ValidationResult!.Errors)
        {
            Logger.LogWarning("Validation error: {ErrorMessage}", error.ErrorMessage);
            await command.Message.Channel.SendMessageAsync(error.ErrorMessage);
        }

        return false;
    }
}