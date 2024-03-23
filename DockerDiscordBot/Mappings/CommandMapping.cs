using DockerDiscordBot.Commands.CreateContainer;
using DockerDiscordBot.Commands.GetContainers;
using DockerDiscordBot.Commands.GetDockerInfo;
using DockerDiscordBot.Commands.Help;
using DockerDiscordBot.Commands.Ping;
using DockerDiscordBot.Commands.RemoveContainer;
using DockerDiscordBot.Commands.RestartContainer;
using DockerDiscordBot.Commands.ShowContainer;
using DockerDiscordBot.Commands.StartContainer;
using DockerDiscordBot.Commands.StopContainer;

namespace DockerDiscordBot.Mappings;

public static class CommandMapping
{
    public static readonly Dictionary<string, Type> Commands = new()
    {
        { "help", typeof(HelpCommand) },
        { "ping", typeof(PingCommand) },
        { "dockerps", typeof(GetContainersCommand) },
        { "dockerstop", typeof(StopContainerCommand) },
        { "dockerstart", typeof(StartContainerCommand) },
        { "dockerrestart", typeof(RestartContainerCommand) },
        { "dockerremove", typeof(RemoveContainerCommand) },
        { "dockershow", typeof(ShowContainerCommand) },
        { "dockercreate", typeof(CreateContainerCommand) },
        { "dockerinfo", typeof(GetDockerInfoCommand) }
    };
}