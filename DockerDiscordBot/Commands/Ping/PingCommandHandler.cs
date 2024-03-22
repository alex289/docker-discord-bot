using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.Ping;

public sealed class PingCommandHandler : IRequestHandler<PingCommand>
{
    private readonly ILogger<PingCommandHandler> _logger;

    public PingCommandHandler(ILogger<PingCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(PingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(PingCommand));
        await request.Message.Channel.SendMessageAsync("pong!");
    }
}