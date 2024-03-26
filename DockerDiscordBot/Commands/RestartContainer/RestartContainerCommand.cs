using Discord.WebSocket;

namespace DockerDiscordBot.Commands.RestartContainer;

public sealed class RestartContainerCommand : Command
{
    private static readonly RestartContainerCommandValidation s_validation = new();
    
    public string ContainerId => Message.Content.Split(" ").LastOrDefault() ?? string.Empty;
    
    public RestartContainerCommand(SocketMessage message) : base(message)
    {
    }
    
    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}