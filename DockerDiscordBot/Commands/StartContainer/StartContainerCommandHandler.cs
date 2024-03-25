using DockerDiscordBot.Interfaces;
using MediatR;
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

        var containerId = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(containerId))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var result = await _dockerService.StartContainerAsync(containerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {containerId} started.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to start container {containerId}.");
        }
    }
}