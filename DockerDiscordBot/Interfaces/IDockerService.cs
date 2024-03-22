using Docker.DotNet.Models;

namespace DockerDiscordBot.Interfaces;

public interface IDockerService
{
    Task<IList<ContainerListResponse>?> GetAllContainersAsync(CancellationToken cancellationToken);
}