using BreakableLime.DockerBackgroundService.models.external;
using Docker.DotNet;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public abstract class DockerActionHandlerBase : IDockerActionHandler
    {
        private readonly IDockerClient _dockerClient;
        private readonly DockerWorkItemBase _actionSpecification;

        internal DockerActionHandlerBase(IDockerClient dockerClient, DockerWorkItemBase actionSpecification)
        {
            _dockerClient = dockerClient;
            _actionSpecification = actionSpecification;
        }
        
        public abstract void Execute();
    }
}