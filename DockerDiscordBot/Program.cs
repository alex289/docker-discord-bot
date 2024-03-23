using System.Reflection;
using Discord;
using Discord.WebSocket;
using DockerDiscordBot.Extensions;
using DockerDiscordBot.Interfaces;
using DockerDiscordBot.Services;
using DockerDiscordBot.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DockerDiscordBot;

public sealed class Program
{
    private static ServiceProvider _services = null!;
    private static IConfiguration _configuration = null!;

    public static async Task Main(string[] args)
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection = ConfigureServices(serviceCollection);
        _services = serviceCollection.BuildServiceProvider();

        await RunAsync();
    }

    private static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services
            .AddOptions<ApplicationSettings>()
            .Bind(_configuration.GetSection("ApplicationSettings"))
            .Validate(x =>
                !string.IsNullOrWhiteSpace(x.DiscordToken),
                "Discord token is required.")
            .Validate(x =>
                !string.IsNullOrWhiteSpace(x.DockerHost),
                "Docker host is required.")
            .ValidateOnStart();

        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddCommands();

        services.AddScoped<IDockerService, DockerService>();
        services.AddScoped<IDiscordService, DiscordService>();

        services
            .AddSingleton(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers |
                                 GatewayIntents.MessageContent
            })
            .AddSingleton<DiscordSocketClient>();

        services.AddLogging(configure =>
        {
            configure.ClearProviders();
            configure.AddConfiguration(_configuration);
            configure.AddSimpleConsole(options =>
            {
                options.IncludeScopes = false;
                options.TimestampFormat = "[dd.MM.yyyy HH:mm:ss] ";
                options.SingleLine = true;
            });
        });

        return services;
    }

    private static async Task RunAsync()
    {
        var logger = _services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Bot starting...");

        var client = _services.GetRequiredService<IDiscordService>();
        await client.StartAsync();

        await Task.Delay(Timeout.Infinite);
    }
}