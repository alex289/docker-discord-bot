using Discord;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Extensions;

public static class LoggerExtensions
{
    public static Task LogMessageAsync<T>(this ILogger<T> logger, LogMessage message)
    {
        switch (message.Severity)
        {
            case LogSeverity.Critical:
            case LogSeverity.Error:
                logger.LogError(message.Message, message.Exception);
                break;
            case LogSeverity.Warning:
                logger.LogWarning(message.Message);
                break;
            case LogSeverity.Debug:
                logger.LogDebug(message.Message);
                break;
            case LogSeverity.Info:
            case LogSeverity.Verbose:
                logger.LogInformation(message.Message);
                break;
            default:
                logger.LogInformation(message.Message);
                break;
        }

        return Task.CompletedTask;
    }
}