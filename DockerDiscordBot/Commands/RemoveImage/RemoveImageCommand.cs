using Discord.WebSocket;

namespace DockerDiscordBot.Commands.RemoveImage;

public sealed class RemoveImageCommand : Command
{
    private static readonly RemoveImageCommandValidation s_validation = new();
    
    public string ImageId => Message.Content.Split(" ").LastOrDefault() ?? string.Empty;
    
    public RemoveImageCommand(SocketMessage message) : base(message)
    {
    }
    
    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}