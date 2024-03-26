using Discord.WebSocket;

namespace DockerDiscordBot.Commands.ShowContainer;

public sealed class ShowContainerCommand : Command
{
    private static readonly ShowContainerCommandValidation s_validation = new();
    
    public string ContainerId => Message.Content.Split(" ").LastOrDefault() ?? string.Empty;
    
    public ShowContainerCommand(SocketMessage message) : base(message)
    {
    }
    
    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}