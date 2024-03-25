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

        var containerId = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(containerId))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var result = await _dockerService.RestartContainerAsync(containerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {containerId} restarted.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to restart container {containerId}.");
        }
    }
}