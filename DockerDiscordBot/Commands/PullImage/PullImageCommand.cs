using Discord.WebSocket;

namespace DockerDiscordBot.Commands.PullImage;

public sealed class PullImageCommand : Command
{
    private static readonly PullImageCommandValidation s_validation = new();

    public string ImageId => Message.Content.Split(" ").LastOrDefault() ?? string.Empty;

    public PullImageCommand(SocketMessage message) : base(message)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}