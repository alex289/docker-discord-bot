using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.CreateContainer;

public sealed class CreateContainerCommandHandler : IRequestHandler<CreateContainerCommand>
{
    private readonly IDockerService _dockerService;
    private readonly ILogger<CreateContainerCommandHandler> _logger;

    public CreateContainerCommandHandler(ILogger<CreateContainerCommandHandler> logger, IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task Handle(CreateContainerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(CreateContainerCommand));

        var parameters = request.Message.Content.Split(" ");

        var imageId = parameters.ElementAtOrDefault(1);
        var containerName = parameters.ElementAtOrDefault(2);

        if (string.IsNullOrWhiteSpace(imageId))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        if (string.IsNullOrWhiteSpace(containerName))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var containerId = await _dockerService.CreateContainerAsync(
            imageId,
            containerName,
            cancellationToken);

        if (containerId is null)
        {
            await request.Message.Channel.SendMessageAsync($"Failed to create container {containerName}.");
            return;
        }

        await request.Message.Channel.SendMessageAsync($"Container created: {containerId}");
    }
}