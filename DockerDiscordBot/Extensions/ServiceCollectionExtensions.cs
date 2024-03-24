using DockerDiscordBot.Commands;
using DockerDiscordBot.Commands.CreateContainer;
using DockerDiscordBot.Commands.GetContainers;
using DockerDiscordBot.Commands.GetDockerInfo;
using DockerDiscordBot.Commands.GetImages;
using DockerDiscordBot.Commands.Help;
using DockerDiscordBot.Commands.Ping;
using DockerDiscordBot.Commands.PullImage;
using DockerDiscordBot.Commands.RemoveContainer;
using DockerDiscordBot.Commands.RemoveImage;
using DockerDiscordBot.Commands.RestartContainer;
using DockerDiscordBot.Commands.ShowContainer;
using DockerDiscordBot.Commands.StartContainer;
using DockerDiscordBot.Commands.StopContainer;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DockerDiscordBot.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services
            .AddCommandHandler<PingCommand, PingCommandHandler>()
            .AddCommandHandler<GetContainersCommand, GetContainersCommandHandler>()
            .AddCommandHandler<StopContainerCommand, StopContainerCommandHandler>()
            .AddCommandHandler<StartContainerCommand, StartContainerCommandHandler>()
            .AddCommandHandler<RestartContainerCommand, RestartContainerCommandHandler>()
            .AddCommandHandler<RemoveContainerCommand, RemoveContainerCommandHandler>()
            .AddCommandHandler<GetDockerInfoCommand, GetDockerInfoCommandHandler>()
            .AddCommandHandler<CreateContainerCommand, CreateContainerCommandHandler>()
            .AddCommandHandler<ShowContainerCommand, ShowContainerCommandHandler>()
            .AddCommandHandler<HelpCommand, HelpCommandHandler>()
            .AddCommandHandler<GetImagesCommand, GetImagesCommandHandler>()
            .AddCommandHandler<PullImageCommand, PullImageCommandHandler>()
            .AddCommandHandler<RemoveImageCommand, RemoveImageCommandHandler>();

        return services;
    }

    private static IServiceCollection AddCommandHandler<TCommand, TCommandHandler>(this IServiceCollection services)
        where TCommand : Command
        where TCommandHandler : class, IRequestHandler<TCommand>
    {
        return services.AddScoped<IRequestHandler<TCommand>, TCommandHandler>();
    }
}