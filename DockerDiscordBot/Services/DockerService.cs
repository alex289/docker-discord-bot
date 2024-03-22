using Docker.DotNet;
using Docker.DotNet.Models;
using DockerDiscordBot.Interfaces;
using DockerDiscordBot.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DockerDiscordBot.Services;

public sealed class DockerService : IDockerService
{
    private readonly DockerClient _client;
    private readonly ILogger<DockerService> _logger;

    public DockerService(
        IOptions<ApplicationSettings> options,
        ILogger<DockerService> logger)
    {
        _logger = logger;
        _client = new DockerClientConfiguration(
                new Uri(options.Value.DockerHost))
            .CreateClient();
    }

    public async Task<IList<ContainerListResponse>?> GetAllContainersAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _client.Containers.ListContainersAsync(
                new ContainersListParameters
                {
                    All = true
                },
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get containers.");
            return null;
        }
    }
}