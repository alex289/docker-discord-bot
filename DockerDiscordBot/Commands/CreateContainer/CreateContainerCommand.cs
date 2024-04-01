using Discord.WebSocket;

namespace DockerDiscordBot.Commands.CreateContainer;

public sealed class CreateContainerCommand : Command
{
    private static readonly CreateContainerCommandValidation s_validation = new();

    public string ImageId => Message.Content.Split(" ").ElementAtOrDefault(1) ?? string.Empty;
    public string ContainerName => Message.Content.Split(" ").ElementAtOrDefault(2) ?? string.Empty;
    public string Ports => Message.Content.Split(" ").ElementAtOrDefault(3) ?? string.Empty;

    public CreateContainerCommand(SocketMessage message) : base(message)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}