using BreakableLime.DockerBackgroundService.models.external;
using Docker.DotNet;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public abstract class DockerActionHandlerBase : IDockerActionHandler
    {
        internal readonly IDockerClient DockerClient;
        internal readonly DockerWorkItem ActionSpecification;

        internal DockerActionHandlerBase(IDockerClient dockerClient, DockerWorkItem actionSpecification)
        {
            DockerClient = dockerClient;
            ActionSpecification = actionSpecification;
        }

        public abstract void Execute();
    }
}