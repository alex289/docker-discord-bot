namespace DockerDiscordBot.Settings;

public sealed class ApplicationSettings
{
    public required string DiscordToken { get; init; }
    public required string DockerHost { get; init; }
    public string AdminUser { get; init; } = string.Empty;
    public required string CommandPrefix { get; init; }
    public string DockerRegistryUrl { get; init; } = string.Empty;
    public string DockerRegistryUsername { get; init; } = string.Empty;
    public string DockerRegistryPassword { get; init; } = string.Empty;
}