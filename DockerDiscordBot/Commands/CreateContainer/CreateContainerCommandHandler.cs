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

        var image = parameters.ElementAtOrDefault(1);
        var containerName = parameters.ElementAtOrDefault(2);
        var ports = parameters.ElementAtOrDefault(3);

        if (string.IsNullOrWhiteSpace(image))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        if (string.IsNullOrWhiteSpace(containerName))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var imageWithoutTag = image.Contains(":") ? image.Split(":").First() : image;
        var tag = image.Contains(":") ? image.Split(":").Last() : "latest";

        var imageExists = await _dockerService.ImageExistsAsync(image, cancellationToken);

        if (!imageExists)
        {
            await request.Message.Channel.SendMessageAsync($"Pulling image {imageWithoutTag}:{tag}.");
            var pulled = await _dockerService.PullImageAsync(imageWithoutTag, tag, cancellationToken);

            if (!pulled)
            {
                await request.Message.Channel.SendMessageAsync($"Failed to pull image {imageWithoutTag}:{tag}.");
                return;
            }
        }

        var portMapping = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(ports))
        {
            var portPairs = ports.Split(",");

            foreach (var portPair in portPairs)
            {
                var port = portPair.Split(":");
                portMapping.Add(port.First(), port.Last());
            }
        }

        var containerId = await _dockerService.CreateContainerAsync(
            imageWithoutTag + ":" + tag,
            containerName,
            portMapping,
            cancellationToken);

        if (containerId is null)
        {
            await request.Message.Channel.SendMessageAsync($"Failed to create container {containerName}.");
            return;
        }

        await request.Message.Channel.SendMessageAsync($"Container created: {containerId}");
    }
}