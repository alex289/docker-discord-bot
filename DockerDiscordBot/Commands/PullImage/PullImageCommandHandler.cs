using DockerDiscordBot.Interfaces;
using MediatR;
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

        var image = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(image))
        {
            await request.Message.Channel.SendMessageAsync("Please provide an image.");
            return;
        }

        var imageWithoutTag = image.Contains(":") ? image.Split(":")[0] : image;
        var tag = image.Contains(":") ? image.Split(":")[^1] : "latest";

        await request.Message.Channel.SendMessageAsync($"Pulling image {imageWithoutTag}:{tag}.");

        var result = await _dockerService.PullImageAsync(
            image,
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