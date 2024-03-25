using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.Ping;

public sealed class PingCommandHandler : CommandHandler<PingCommand>
{
    public PingCommandHandler(ILogger<PingCommandHandler> logger) : base(logger)
    {
    }

    public override async Task Handle(PingCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(PingCommand));
        await request.Message.Channel.SendMessageAsync("pong!");
    }
}