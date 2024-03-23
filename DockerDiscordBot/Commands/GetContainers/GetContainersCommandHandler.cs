using Discord;
using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.GetContainers;

public sealed class GetContainersCommandHandler : IRequestHandler<GetContainersCommand>
{
    private readonly IDockerService _dockerService;
    private readonly ILogger<GetContainersCommandHandler> _logger;

    public GetContainersCommandHandler(
        ILogger<GetContainersCommandHandler> logger,
        IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task Handle(GetContainersCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(GetContainersCommand));

        var containers = await _dockerService.GetAllContainersAsync(cancellationToken);

        if (containers is null)
        {
            await request.Message.Channel.SendMessageAsync("Failed to get containers.");
            return;
        }

        if (containers.Count == 0)
        {
            await request.Message.Channel.SendMessageAsync("No containers found.");
            return;
        }

        foreach (var container in containers)
        {
            var embed = new EmbedBuilder()
                .WithTitle(container.Names.FirstOrDefault())
                .AddField("ID", container.ID)
                .AddField("Image", container.Image)
                .AddField("State", container.State)
                .AddField("State", container.Status)
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();

            await request.Message.Channel.SendMessageAsync(embed: embed);
        }
    }
}