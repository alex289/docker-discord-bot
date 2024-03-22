namespace DockerDiscordBot.Settings;

public sealed class ApplicationSettings
{
    public required string DiscordToken { get; init; }
    public required string DockerHost { get; init; }
}