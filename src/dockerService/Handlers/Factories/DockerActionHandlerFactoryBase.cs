using BreakableLime.DockerBackgroundService.models.external;
using Docker.DotNet;

namespace BreakableLime.DockerBackgroundService.Handlers.Factories
{
    public abstract class DockerActionHandlerFactoryBase : IDockerActionHandlerFactory
    {
        protected readonly DockerClient DockerClient;

        protected DockerActionHandlerFactoryBase(DockerClient dockerClient)
        {
            DockerClient = dockerClient;
        }
        
        public abstract IDockerActionHandler Create(DockerWorkItem actionSpecification);
    }
}