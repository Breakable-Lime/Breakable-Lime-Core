using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.DockerBackgroundService.models.external.ActionSpecifications;
using Docker.DotNet;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public class FetchImageHandler : DockerActionHandlerBase
    {

        private readonly FetchImageSpecification _specs;
        
        public FetchImageHandler(IDockerClient dockerClient, DockerWorkItem actionSpecification) : base(dockerClient, actionSpecification)
        {
            _specs = actionSpecification.SpecificationsMarker as FetchImageSpecification;
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}