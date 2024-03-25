using Discord;
using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.ShowContainer;

public sealed class ShowContainerCommandHandler : CommandHandler<ShowContainerCommand>
{
    private readonly IDockerService _dockerService;

    public ShowContainerCommandHandler(
        ILogger<ShowContainerCommandHandler> logger,
        IDockerService dockerService) : base(logger)
    {
        _dockerService = dockerService;
    }

    public override async Task Handle(ShowContainerCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(ShowContainerCommand));

        var containerId = request.Message.Content.Split(" ").LastOrDefault();

        if (string.IsNullOrWhiteSpace(containerId))
        {
            await request.Message.Channel.SendMessageAsync("Please provide a container id.");
            return;
        }

        var result = await _dockerService.GetContainerAsync(containerId, cancellationToken);

        if (result is null)
        {
            await request.Message.Channel.SendMessageAsync($"Failed to get container {containerId}.");
            return;
        }

        var embed = new EmbedBuilder()
            .WithTitle($"Container {result.Name}")
            .AddField("ID", result.ID)
            .AddField("Name", result.Name)
            .AddField("Image", result.Image)
            .AddField("State", result.State.Status)
            .AddField("Created", result.Created)
            .WithColor(Color.Blue)
            .WithCurrentTimestamp()
            .Build();

        await request.Message.Channel.SendMessageAsync(embed: embed);
    }
}