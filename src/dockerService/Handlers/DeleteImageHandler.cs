using System.Threading.Tasks;
using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.DockerBackgroundService.models.external.ActionSpecifications;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public class DeleteImageHandler : DockerActionHandlerBase
    {
        private readonly DeleteImageSpecification _spec;

        public DeleteImageHandler(IDockerClient dockerClient, DockerWorkItem actionSpecification) : base(dockerClient, actionSpecification)
        {
            _spec = actionSpecification.SpecificationsMarker as DeleteImageSpecification;
        }

        public override Task Execute()
        {
            
            throw new System.NotImplementedException();
        }
    }
}