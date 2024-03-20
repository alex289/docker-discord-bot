using System.Reflection;
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

        await Task.Delay(Timeout.Infinite);
    }
}
