using Docker.DotNet.Models;

namespace DockerDiscordBot.Interfaces;

public interface IDockerService
{
    Task<IList<ContainerListResponse>?> GetAllContainersAsync(CancellationToken cancellationToken);
    Task<bool> StopContainerAsync(string containerId, CancellationToken cancellationToken);
    Task<bool> StartContainerAsync(string containerId, CancellationToken cancellationToken);
    Task<bool> RestartContainerAsync(string containerId, CancellationToken cancellationToken);
    Task<bool> RemoveContainerAsync(string containerId, CancellationToken cancellationToken);
}