using BreakableLime.DockerBackgroundService.models.external;
using Docker.DotNet;

namespace BreakableLime.DockerBackgroundService.Handlers.Factories
{
    public class CreateContainerHandlerFactory : DockerActionHandlerFactoryBase
    {
        public CreateContainerHandlerFactory(DockerClient dockerClient) : base(dockerClient)
        { }

        public override IDockerActionHandler Create(DockerWorkItem actionSpecification) =>
            new CreateContainerHandler(DockerClient, actionSpecification);


    }
}