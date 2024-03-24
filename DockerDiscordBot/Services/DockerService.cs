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
    private readonly ApplicationSettings _options;

    public DockerService(
        IOptions<ApplicationSettings> options,
        ILogger<DockerService> logger)
    {
        _logger = logger;
        _client = new DockerClientConfiguration(
                new Uri(options.Value.DockerHost))
            .CreateClient();
        _options = options.Value;
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

    public async Task<bool> StopContainerAsync(string containerId, CancellationToken cancellationToken)
    {
        try
        {
            return await _client.Containers.StopContainerAsync(
                containerId,
                new ContainerStopParameters(),
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to stop container {ContainerId}.", containerId);
            return false;
        }
    }

    public async Task<bool> StartContainerAsync(string containerId, CancellationToken cancellationToken)
    {
        try
        {
            return await _client.Containers.StartContainerAsync(
                containerId,
                new ContainerStartParameters(),
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start container {ContainerId}.", containerId);
            return false;
        }
    }

    public async Task<bool> RestartContainerAsync(string containerId, CancellationToken cancellationToken)
    {
        try
        {
            await _client.Containers.RestartContainerAsync(
                containerId,
                new ContainerRestartParameters(),
                cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start container {ContainerId}.", containerId);
            return false;
        }
    }

    public async Task<bool> RemoveContainerAsync(string containerId, CancellationToken cancellationToken)
    {
        try
        {
            await _client.Containers.RemoveContainerAsync(
                containerId,
                new ContainerRemoveParameters(),
                cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to remove container {ContainerId}.", containerId);
            return false;
        }
    }

    public async Task<string?> CreateContainerAsync(
        string image,
        string name,
        Dictionary<string, string> ports,
        CancellationToken cancellationToken)
    {
        var exposedPorts = new Dictionary<string, EmptyStruct>();
        var portBindings = new Dictionary<string, IList<PortBinding>>();

        foreach (var (key, value) in ports)
        {
            exposedPorts[key] = default;
            portBindings[key] = new List<PortBinding>
            {
                new()
                {
                    HostPort = value
                }
            };
        }

        try
        {
            var result = await _client.Containers.CreateContainerAsync(
                new CreateContainerParameters
                {
                    Image = image,
                    Name = name,
                    ExposedPorts = exposedPorts,
                    HostConfig = new HostConfig
                    {
                        PortBindings = portBindings,
                        PublishAllPorts = true
                    }
                },
                cancellationToken);
            return result.ID;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start container {ContainerName}.", name);
            return null;
        }
    }

    public async Task<ContainerInspectResponse?> GetContainerAsync(string containerId,
        CancellationToken cancellationToken)
    {
        try
        {
            return await _client.Containers.InspectContainerAsync(
                containerId,
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get container {ContainerId}.", containerId);
            return null;
        }
    }

    public async Task<SystemInfoResponse?> GetDockerInfoAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _client.System.GetSystemInfoAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get docker system information.");
            return null;
        }
    }

    public async Task<bool> PullImageAsync(string image, string tag, CancellationToken cancellationToken)
    {
        try
        {
            await _client.Images.CreateImageAsync(
                new ImagesCreateParameters
                {
                    FromImage = image,
                    Tag = tag
                },
                new AuthConfig
                {
                    ServerAddress = _options.DockerRegistryUrl,
                    Username = _options.DockerRegistryUsername,
                    Password = _options.DockerRegistryPassword
                },
                new Progress<JSONMessage>(),
                cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to pull image {Image}:{Tag}.", image, tag);
            return false;
        }
    }

    public async Task<bool> RemoveImageAsync(string image, CancellationToken cancellationToken)
    {
        try
        {
            await _client.Images.DeleteImageAsync(
                image,
                new ImageDeleteParameters(),
                cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to pull image {Image}.", image);
            return false;
        }
    }

    public async Task<IList<ImagesListResponse>?> GetAllImagesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _client.Images.ListImagesAsync(
                new ImagesListParameters(),
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get images.");
            return null;
        }
    }

    public async Task<bool> ImageExistsAsync(string image, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _client.Images.InspectImageAsync(
                image,
                cancellationToken);
            return result is not null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to find image {Image}.", image);
            return false;
        }
    }
}