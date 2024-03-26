using DockerDiscordBot.Interfaces;
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

        if (!await TestValidityAsync(request))
        {
            return;
        }

        var result = await _dockerService.RemoveImageAsync(request.ImageId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Image {request.ImageId} removed.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to remove image {request.ImageId}.");
        }
    }
}