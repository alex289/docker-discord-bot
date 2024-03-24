using Docker.DotNet.Models;

namespace DockerDiscordBot.Interfaces;

public interface IDockerService
{
    Task<IList<ContainerListResponse>?> GetAllContainersAsync(CancellationToken cancellationToken);
    Task<bool> StopContainerAsync(string containerId, CancellationToken cancellationToken);
    Task<bool> StartContainerAsync(string containerId, CancellationToken cancellationToken);
    Task<bool> RestartContainerAsync(string containerId, CancellationToken cancellationToken);
    Task<bool> RemoveContainerAsync(string containerId, CancellationToken cancellationToken);

    Task<string?> CreateContainerAsync(
        string image,
        string name,
        CancellationToken cancellationToken);

    Task<ContainerInspectResponse?> GetContainerAsync(string containerId, CancellationToken cancellationToken);
    Task<SystemInfoResponse?> GetDockerInfoAsync(CancellationToken cancellationToken);

    Task<bool> PullImageAsync(string image, string tag, CancellationToken cancellationToken);
    Task<bool> RemoveImageAsync(string image, CancellationToken cancellationToken);
    Task<IList<ImagesListResponse>?> GetAllImagesAsync(CancellationToken cancellationToken);
    Task<bool> ImageExistsAsync(string image, CancellationToken cancellationToken);
}