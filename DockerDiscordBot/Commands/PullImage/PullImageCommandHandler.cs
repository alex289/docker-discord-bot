using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.PullImage;

public sealed class PullImageCommandHandler : IRequestHandler<PullImageCommand>
{
    private readonly IDockerService _dockerService;
    private readonly ILogger<PullImageCommandHandler> _logger;

    public PullImageCommandHandler(ILogger<PullImageCommandHandler> logger, IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task Handle(PullImageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(PullImageCommand));

        var image = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(image))
        {
            await request.Message.Channel.SendMessageAsync("Please provide an image.");
            return;
        }

        var imageWithoutTag = image.Contains(":") ? image.Split(":").First() : image;
        var tag = image.Contains(":") ? image.Split(":").Last() : "latest";

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