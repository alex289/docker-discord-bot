using Discord.WebSocket;
using DockerDiscordBot.Commands;
using DockerDiscordBot.Commands.CreateContainer;
using DockerDiscordBot.Commands.GetContainers;
using DockerDiscordBot.Commands.GetDockerInfo;
using DockerDiscordBot.Commands.Ping;
using DockerDiscordBot.Commands.RemoveContainer;
using DockerDiscordBot.Commands.RestartContainer;
using DockerDiscordBot.Commands.ShowContainer;
using DockerDiscordBot.Commands.StartContainer;
using DockerDiscordBot.Commands.StopContainer;

namespace DockerDiscordBot.Extensions;

public static class CommandsExtensions
{
    private static readonly string s_prefix = "!";

    private static readonly Dictionary<string, Type> s_commands = new()
    {
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

    public static Command? GetCommand(this SocketMessage message)
    {
        if (!message.Content.StartsWith(s_prefix))
        {
            return null;
        }

        var command = message.Content.Substring(1).Split(' ')[0];

        if (!s_commands.ContainsKey(command))
        {
            return null;
        }

        var commandType = s_commands[command];

        return (Command)Activator.CreateInstance(commandType, message)!;
    }
}