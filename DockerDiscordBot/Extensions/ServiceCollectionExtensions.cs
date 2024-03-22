using DockerDiscordBot.Commands;
using DockerDiscordBot.Commands.GetContainers;
using DockerDiscordBot.Commands.Ping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DockerDiscordBot.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services
            .AddCommandHandler<PingCommand, PingCommandHandler>()
            .AddCommandHandler<GetContainersCommand, GetContainersCommandHandler>();

        return services;
    }

    private static IServiceCollection AddCommandHandler<TCommand, TCommandHandler>(this IServiceCollection services)
        where TCommand : Command
        where TCommandHandler : class, IRequestHandler<TCommand>
    {
        return services.AddScoped<IRequestHandler<TCommand>, TCommandHandler>();
    }
}