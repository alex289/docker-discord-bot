using DockerDiscordBot.Interfaces;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.RemoveContainer;

public sealed class RemoveContainerCommandHandler : CommandHandler<RemoveContainerCommand>
{
    private readonly IDockerService _dockerService;

    public RemoveContainerCommandHandler(
        ILogger<RemoveContainerCommandHandler> logger,
        IDockerService dockerService) : base(logger)
    {
        _dockerService = dockerService;
    }

    public override async Task Handle(RemoveContainerCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(RemoveContainerCommand));

        if (!await TestValidityAsync(request))
        {
            return;
        }

        var result = await _dockerService.RemoveContainerAsync(request.ContainerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {request.ContainerId} removed.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to remove container {request.ContainerId}.");
        }
    }
}