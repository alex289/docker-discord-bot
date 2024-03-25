using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.RemoveImage;

public sealed class RemoveImageCommandHandler : CommandHandler<RemoveImageCommand>
{
    private readonly IDockerService _dockerService;

    public RemoveImageCommandHandler(
        ILogger<RemoveImageCommandHandler> logger,
        IDockerService dockerService) : base(logger)
    {
        _dockerService = dockerService;
    }

    public override async Task Handle(RemoveImageCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(RemoveImageCommand));

        var image = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(image))
        {
            await request.Message.Channel.SendMessageAsync("Please provide an image.");
            return;
        }

        var result = await _dockerService.RemoveImageAsync(image, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Image {image} removed.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to remove image {image}.");
        }
    }
}