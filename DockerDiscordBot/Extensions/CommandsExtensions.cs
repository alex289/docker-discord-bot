using Discord.WebSocket;
using DockerDiscordBot.Commands;
using DockerDiscordBot.Commands.Ping;

namespace DockerDiscordBot.Extensions;

public static class CommandsExtensions
{
    private static readonly string s_prefix = "!";
    
    private static readonly Dictionary<string, Type> s_commands = new()
    {
        { "ping", typeof(PingCommand) }
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