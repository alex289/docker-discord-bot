using Discord;
using DockerDiscordBot.Mappings;
using DockerDiscordBot.Settings;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DockerDiscordBot.Commands.Help;

public sealed class HelpCommandHandler : IRequestHandler<HelpCommand>
{
    private readonly string _commandPrefix;
    private readonly ILogger<HelpCommandHandler> _logger;

    public HelpCommandHandler(
        ILogger<HelpCommandHandler> logger,
        IOptions<ApplicationSettings> _options)
    {
        _logger = logger;
        _commandPrefix = _options.Value.CommandPrefix;
    }

    public async Task Handle(HelpCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(HelpCommand));

        var commands = CommandMapping.Commands.Keys.Select(key => $"`{_commandPrefix}{key}`").ToList();

        var embed = new EmbedBuilder()
            .WithTitle("Available Commands")
            .WithDescription(string.Join(", ", commands))
            .WithColor(Color.Blue)
            .Build();

        await request.Message.Channel.SendMessageAsync(embed: embed);
    }
}