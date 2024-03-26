using DockerDiscordBot.Interfaces;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.StartContainer;

public sealed class StartContainerCommandHandler : CommandHandler<StartContainerCommand>
{
    private readonly IDockerService _dockerService;

    public StartContainerCommandHandler(
        ILogger<StartContainerCommandHandler> logger,
        IDockerService dockerService) : base(logger)
    {
        _dockerService = dockerService;
    }

    public override async Task Handle(StartContainerCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(StartContainerCommand));
        
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var result = await _dockerService.StartContainerAsync(request.ContainerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {request.ContainerId} started.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to start container {request.ContainerId}.");
        }
    }
}