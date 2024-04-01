using Discord.WebSocket;

namespace DockerDiscordBot.Commands.StopContainer;

public sealed class StopContainerCommand : Command
{
    private static readonly StopContainerCommandValidation s_validation = new();

    public string ContainerId => Message.Content.Split(" ").LastOrDefault() ?? string.Empty;

    public StopContainerCommand(SocketMessage message) : base(message)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}