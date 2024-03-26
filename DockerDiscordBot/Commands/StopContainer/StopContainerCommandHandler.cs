using DockerDiscordBot.Interfaces;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.StopContainer;

public sealed class StopContainerCommandHandler : CommandHandler<StopContainerCommand>
{
    private readonly IDockerService _dockerService;

    public StopContainerCommandHandler(
        ILogger<StopContainerCommandHandler> logger,
        IDockerService dockerService) : base(logger)
    {
        _dockerService = dockerService;
    }

    public override async Task Handle(StopContainerCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(StopContainerCommand));
        
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var result = await _dockerService.StopContainerAsync(request.ContainerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {request.ContainerId} stopped.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to stop container {request.ContainerId}.");
        }
    }
}