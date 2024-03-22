using Discord;
using Discord.WebSocket;
using DockerDiscordBot.Extensions;
using DockerDiscordBot.Interfaces;
using DockerDiscordBot.Settings;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DockerDiscordBot.Services;

public sealed class DiscordService : IDiscordService
{
    private readonly IMediator _bus;
    private readonly DiscordSocketClient _client;
    private readonly ILogger<DiscordService> _logger;
    private readonly string _token;

    public DiscordService(
        DiscordSocketClient client,
        ILogger<DiscordService> logger,
        IOptions<ApplicationSettings> options, IMediator bus)
    {
        _client = client;
        _logger = logger;
        _bus = bus;
        _token = options.Value.DiscordToken;
    }

    public async Task StartAsync()
    {
        _client.Log += _logger.LogMessageAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += MessageReceivedAsync;

        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();
    }

    private Task ReadyAsync()
    {
        _logger.LogInformation("{CurrentUser} is connected!", _client.CurrentUser);
        return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        // The bot should never respond to itself.
        if (message.Author.Id == _client.CurrentUser.Id)
        {
            return;
        }

        var command = message.GetCommand();

        if (command is null)
        {
            _logger.LogWarning("Unhandled command: {Command}", message.Content);
            return;
        }

        await _bus.Send(command);
    }
}