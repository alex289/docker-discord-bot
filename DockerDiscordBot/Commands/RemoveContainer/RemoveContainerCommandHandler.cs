using DockerDiscordBot.Interfaces;
using MediatR;
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

        var containerId = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(containerId))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var result = await _dockerService.RemoveContainerAsync(containerId, cancellationToken);

        if (result)
        {
            await request.Message.Channel.SendMessageAsync($"Container {containerId} removed.");
        }
        else
        {
            await request.Message.Channel.SendMessageAsync($"Failed to remove container {containerId}.");
        }
    }
}