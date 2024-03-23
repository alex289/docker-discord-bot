namespace DockerDiscordBot.Settings;

public sealed class ApplicationSettings
{
    public required string DiscordToken { get; init; }
    public required string DockerHost { get; init; }
    public string AdminUser { get; init; } = string.Empty;
    public required string CommandPrefix { get; init; }
}