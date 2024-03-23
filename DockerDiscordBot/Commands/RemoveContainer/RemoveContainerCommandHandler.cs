using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.RemoveContainer;

public sealed class RemoveContainerCommandHandler : IRequestHandler<RemoveContainerCommand>
{
    private readonly IDockerService _dockerService;
    private readonly ILogger<RemoveContainerCommandHandler> _logger;

    public RemoveContainerCommandHandler(ILogger<RemoveContainerCommandHandler> logger, IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task Handle(RemoveContainerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(RemoveContainerCommand));

        var containerId = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(containerId))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var result = await _dockerService.RemoveContainerAsync(containerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {containerId} removed.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to remove container {containerId}.");
        }
    }
}