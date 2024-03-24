using Discord;
using DockerDiscordBot.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.GetImages;

public sealed class GetImagesCommandHandler : IRequestHandler<GetImagesCommand>
{
    private readonly IDockerService _dockerService;
    private readonly ILogger<GetImagesCommandHandler> _logger;

    public GetImagesCommandHandler(ILogger<GetImagesCommandHandler> logger, IDockerService dockerService)
    {
        _logger = logger;
        _dockerService = dockerService;
    }

    public async Task Handle(GetImagesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(GetImagesCommand));

        var images = await _dockerService.GetAllImagesAsync(cancellationToken);

        if (images is null)
        {
            await request.Message.Channel.SendMessageAsync("Failed to get images.");
            return;
        }

        if (images.Count == 0)
        {
            await request.Message.Channel.SendMessageAsync("No images found.");
            return;
        }

        foreach (var image in images)
        {
            var size = image.Size switch
            {
                < 1024 => $"{image.Size} B",
                < 1024 * 1024 => $"{image.Size / 1024} KB",
                _ => $"{image.Size / 1024 / 1024} MB"
            };
            var embed = new EmbedBuilder()
                .WithTitle(image.RepoTags.FirstOrDefault())
                .AddField("ID", image.ID)
                .AddField("Created", image.Created)
                .AddField("Size", size)
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();

            await request.Message.Channel.SendMessageAsync(embed: embed);
        }
    }
}