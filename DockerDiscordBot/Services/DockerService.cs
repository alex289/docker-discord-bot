using Docker.DotNet;
using DockerDiscordBot.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DockerDiscordBot.Services;

public sealed class DockerService : IDockerService
{
    private readonly DockerClient _client;

    public DockerService(IConfiguration configuration)
    {
        _client = new DockerClientConfiguration(new Uri(
                configuration.GetSection("Discord").GetValue<string?>("Token")!))
            .CreateClient();
    }
}