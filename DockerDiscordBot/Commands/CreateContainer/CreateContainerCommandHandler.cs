using DockerDiscordBot.Interfaces;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.CreateContainer;

public sealed class CreateContainerCommandHandler : CommandHandler<CreateContainerCommand>
{
    private readonly IDockerService _dockerService;

    public CreateContainerCommandHandler(
        ILogger<CreateContainerCommandHandler> logger,
        IDockerService dockerService) : base(logger)
    {
        _dockerService = dockerService;
    }

    public override async Task Handle(CreateContainerCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(CreateContainerCommand));

        if (!await TestValidityAsync(request))
        {
            return;
        }

        var imageWithoutTag = request.ImageId.Contains(":") ? request.ImageId.Split(":")[0] : request.ImageId;
        var tag = request.ImageId.Contains(":") ? request.ImageId.Split(":")[^1] : "latest";

        var imageExists = await _dockerService.ImageExistsAsync(request.ImageId, cancellationToken);

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

        if (!string.IsNullOrWhiteSpace(request.Ports))
        {
            var portPairs = request.Ports.Split(",");

            foreach (var portPair in portPairs)
            {
                var port = portPair.Split(":");
                portMapping.Add(port[0], port[^1]);
            }
        }

        var containerId = await _dockerService.CreateContainerAsync(
            imageWithoutTag + ":" + tag,
            request.ContainerName,
            portMapping,
            cancellationToken);

        if (containerId is null)
        {
            await request.Message.Channel.SendMessageAsync($"Failed to create container {request.ContainerName}.");
            return;
        }

        await request.Message.Channel.SendMessageAsync($"Container created: {containerId}");
    }
}