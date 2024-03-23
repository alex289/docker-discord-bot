using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.StartContainer;

public sealed class StartContainerCommandHandler : IRequestHandler<StartContainerCommand>
{
    private readonly IDockerService _dockerService;
    private readonly ILogger<StartContainerCommandHandler> _logger;

    public StartContainerCommandHandler(
        ILogger<StartContainerCommandHandler> logger,
        IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task Handle(StartContainerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(StartContainerCommand));

        var containerId = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(containerId))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var result = await _dockerService.StartContainerAsync(containerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {containerId} started.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to start container {containerId}.");
        }
    }
}