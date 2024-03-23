using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.StopContainer;

public sealed class StopContainerCommandHandler : IRequestHandler<StopContainerCommand>
{
    private readonly IDockerService _dockerService;
    private readonly ILogger<StopContainerCommandHandler> _logger;

    public StopContainerCommandHandler(
        ILogger<StopContainerCommandHandler> logger,
        IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task Handle(StopContainerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(StopContainerCommand));

        var containerId = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(containerId))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var result = await _dockerService.StopContainerAsync(containerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {containerId} stopped.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to stop container {containerId}.");
        }
    }
}