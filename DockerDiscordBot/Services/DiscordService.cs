using Discord;
using Discord.WebSocket;
using DockerDiscordBot.Extensions;
using DockerDiscordBot.Interfaces;
using DockerDiscordBot.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DockerDiscordBot.Services;

public sealed class DiscordService : IDiscordService
{
    private readonly ILogger<DiscordService> _logger;
    private readonly DiscordSocketClient _client;
    private readonly string _token;

    public DiscordService(
        DiscordSocketClient client,
        ILogger<DiscordService> logger,
        IOptions<ApplicationSettings> options)
    {
        _client = client;
        _logger = logger;
        _token = options.Value.DiscordToken;
    }

    public async Task StartAsync()
    {
        _client.Log += _logger.LogMessageAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += MessageReceivedAsync;
        _client.InteractionCreated += InteractionCreatedAsync;

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


        if (message.Content == "!ping")
        {
            _logger.LogInformation("Received a ping command!");
            var cb = new ComponentBuilder()
                .WithButton("Click me!", "unique-id");

            await message.Channel.SendMessageAsync("pong!", components: cb.Build());
        }
    }

    private static async Task InteractionCreatedAsync(SocketInteraction interaction)
    {
        if (interaction is SocketMessageComponent component)
        {
            if (component.Data.CustomId == "unique-id")
            {
                await interaction.RespondAsync("Thank you for clicking my button!");
            }
            else
            {
                Console.WriteLine("An ID has been received that has no handler!");
            }
        }
    }
}