using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.DockerBackgroundService.models.external.ActionSpecifications;
using Docker.DotNet;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public class DeleteImageHandler : DockerActionHandlerBase
    {
        private readonly DeleteImageSpecification _spec;

        public DeleteImageHandler(IDockerClient dockerClient, DockerWorkItem actionSpecification) : base(dockerClient, actionSpecification)
        {
            _spec = actionSpecification.SpecificationsMarker as DeleteImageSpecification;
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}