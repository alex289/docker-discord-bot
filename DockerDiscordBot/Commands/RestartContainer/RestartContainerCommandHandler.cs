using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.RestartContainer;

public sealed class RestartContainerCommandHandler : IRequestHandler<RestartContainerCommand>
{
    private readonly IDockerService _dockerService;
    private readonly ILogger<RestartContainerCommandHandler> _logger;

    public RestartContainerCommandHandler(ILogger<RestartContainerCommandHandler> logger, IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task Handle(RestartContainerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(RestartContainerCommand));

        var containerId = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(containerId))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var result = await _dockerService.RestartContainerAsync(containerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {containerId} restarted.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to restart container {containerId}.");
        }
    }
}