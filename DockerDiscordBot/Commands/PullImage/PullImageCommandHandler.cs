using DockerDiscordBot.Interfaces;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.PullImage;

public sealed class PullImageCommandHandler : CommandHandler<PullImageCommand>
{
    private readonly IDockerService _dockerService;

    public PullImageCommandHandler(
        ILogger<PullImageCommandHandler> logger,
        IDockerService dockerService) : base(logger)
    {
        _dockerService = dockerService;
    }

    public override async Task Handle(PullImageCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(PullImageCommand));

        if (!await TestValidityAsync(request))
        {
            return;
        }

        var imageWithoutTag = request.ImageId.Contains(":") ? request.ImageId.Split(":")[0] : request.ImageId;
        var tag = request.ImageId.Contains(":") ? request.ImageId.Split(":")[^1] : "latest";

        await request.Message.Channel.SendMessageAsync($"Pulling image {imageWithoutTag}:{tag}.");

        var result = await _dockerService.PullImageAsync(
            request.ImageId,
            tag,
            cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Image {imageWithoutTag} pulled.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to pull image {imageWithoutTag}.");
        }
    }
}