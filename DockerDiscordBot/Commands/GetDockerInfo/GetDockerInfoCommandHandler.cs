using Discord;
using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.GetDockerInfo;

public sealed class GetDockerInfoCommandHandler : IRequestHandler<GetDockerInfoCommand>
{
    private readonly IDockerService _dockerService;
    private readonly ILogger<GetDockerInfoCommandHandler> _logger;

    public GetDockerInfoCommandHandler(ILogger<GetDockerInfoCommandHandler> logger, IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task Handle(GetDockerInfoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(GetDockerInfoCommand));


        var result = await _dockerService.GetDockerInfoAsync(cancellationToken);

        if (result is null)
        {
            await request.Message.Channel.SendMessageAsync("Failed to get docker info.");
            return;
        }

        var embed = new EmbedBuilder()
            .WithTitle("Docker Info")
            .AddField("Name", result.Name)
            .AddField("Containers", result.Containers)
            .AddField("Images", result.Images)
            .AddField("Server Version", result.ServerVersion)
            .AddField("Operating System", result.OperatingSystem)
            .AddField("Kernel Version", result.KernelVersion)
            .AddField("Server Version", result.ServerVersion)
            .WithColor(Color.Blue)
            .WithCurrentTimestamp()
            .Build();

        await request.Message.Channel.SendMessageAsync(embed: embed);
    }
}