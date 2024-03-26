using DockerDiscordBot.Interfaces;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.RestartContainer;

public sealed class RestartContainerCommandHandler : CommandHandler<RestartContainerCommand>
{
    private readonly IDockerService _dockerService;

    public RestartContainerCommandHandler(
        ILogger<RestartContainerCommandHandler> logger,
        IDockerService dockerService) : base(logger)
    {
        _dockerService = dockerService;
    }

    public override async Task Handle(RestartContainerCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(RestartContainerCommand));

        if (!await TestValidityAsync(request))
        {
            return;
        }

        var result = await _dockerService.RestartContainerAsync(request.ContainerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {request.ContainerId} restarted.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to restart container {request.ContainerId}.");
        }
    }
}