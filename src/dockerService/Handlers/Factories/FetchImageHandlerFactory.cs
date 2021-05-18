using BreakableLime.DockerBackgroundService.models.external;
using Docker.DotNet;

namespace BreakableLime.DockerBackgroundService.Handlers.Factories
{
    public class FetchImageHandlerFactory: DockerActionHandlerFactoryBase
    {
        public FetchImageHandlerFactory(DockerClient dockerClient) : base(dockerClient)
        {
        }

        public override IDockerActionHandler Create(DockerWorkItem actionSpecification) =>
            new FetchImageHandler(DockerClient, actionSpecification);
    }
}