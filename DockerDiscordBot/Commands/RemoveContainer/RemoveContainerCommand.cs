using Discord.WebSocket;

namespace DockerDiscordBot.Commands.RemoveContainer;

public sealed class RemoveContainerCommand : Command
{
    private static readonly RemoveContainerCommandValidation s_validation = new();
    
    public string ContainerId => Message.Content.Split(" ").LastOrDefault() ?? string.Empty;
    
    public RemoveContainerCommand(SocketMessage message) : base(message)
    {
    }
    
    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}