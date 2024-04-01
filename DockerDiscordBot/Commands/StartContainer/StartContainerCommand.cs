using Discord.WebSocket;

namespace DockerDiscordBot.Commands.StartContainer;

public sealed class StartContainerCommand : Command
{
    private static readonly StartContainerCommandValidation s_validation = new();

    public string ContainerId => Message.Content.Split(" ").LastOrDefault() ?? string.Empty;

    public StartContainerCommand(SocketMessage message) : base(message)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}