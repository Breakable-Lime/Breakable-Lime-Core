using BreakableLime.DockerBackgroundService.models.external;
using Docker.DotNet;

namespace BreakableLime.DockerBackgroundService.Handlers.Factories
{
    public class DeleteImageHandlerFactory : DockerActionHandlerFactoryBase
    {
        public DeleteImageHandlerFactory(DockerClient dockerClient) : base(dockerClient)
        {
        }

        public override IDockerActionHandler Create(DockerWorkItem actionSpecification) =>
            new DeleteImageHandler(DockerClient, actionSpecification);
    }
}