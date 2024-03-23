using Discord;
using DockerDiscordBot.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot.Commands.Help;

public sealed class HelpCommandHandler : IRequestHandler<HelpCommand>
{
    private readonly ILogger<HelpCommandHandler> _logger;

    public HelpCommandHandler(ILogger<HelpCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(HelpCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing {Command}", nameof(HelpCommand));
        
        var commands = CommandMapping.Commands.Keys.Select(key => $"`!{key}`").ToList();
        
        var embed = new EmbedBuilder()
            .WithTitle("Available Commands")
            .WithDescription(string.Join(", ", commands))
            .WithColor(Color.Blue)
            .Build();
        
        await request.Message.Channel.SendMessageAsync(embed: embed);
    }
}