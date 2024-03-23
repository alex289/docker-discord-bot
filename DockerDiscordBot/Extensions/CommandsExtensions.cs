using Discord.WebSocket;
using DockerDiscordBot.Commands;
using DockerDiscordBot.Mappings;

namespace DockerDiscordBot.Extensions;

public static class CommandsExtensions
{
    public static Command? GetCommand(this SocketMessage message, string prefix)
    {
        if (!message.Content.StartsWith(prefix))
        {
            return null;
        }

        var command = message.Content.Substring(1).Split(' ')[0];

        if (!CommandMapping.Commands.ContainsKey(command))
        {
            return null;
        }

        var commandType = CommandMapping.Commands[command];

        return (Command)Activator.CreateInstance(commandType, message)!;
    }
}