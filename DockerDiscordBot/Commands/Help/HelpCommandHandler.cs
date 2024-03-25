using Discord;
using DockerDiscordBot.Mappings;
using DockerDiscordBot.Settings;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DockerDiscordBot.Commands.Help;

public sealed class HelpCommandHandler : CommandHandler<HelpCommand>
{
    private readonly string _commandPrefix;

    public HelpCommandHandler(
        ILogger<HelpCommandHandler> logger,
        IOptions<ApplicationSettings> options) : base(logger)
    {
        _commandPrefix = options.Value.CommandPrefix;
    }

    public override async Task Handle(HelpCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Executing {Command}", nameof(HelpCommand));

        var commands = CommandMapping.Commands.Keys.Select(key => $"`{_commandPrefix}{key}`").ToList();

        var embed = new EmbedBuilder()
            .WithTitle("Available Commands")
            .WithDescription(string.Join(", ", commands))
            .WithColor(Color.Blue)
            .Build();

        await request.Message.Channel.SendMessageAsync(embed: embed);
    }
}