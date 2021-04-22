using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.DockerBackgroundService.models.external.ActionSpecifications;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public class CreateContainerHandler : DockerActionHandlerBase
    {
        private readonly CreateContainerSpecification _specifications;

        public CreateContainerHandler(IDockerClient dockerClient, DockerWorkItem actionSpecification) : base(
            dockerClient, actionSpecification)
        {
            _specifications = actionSpecification.SpecificationsMarker as CreateContainerSpecification;
        }

        public override void Execute()
        {
            var ports = GetExposedPorts();
            
            DockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = _specifications.ImageHash,
                ExposedPorts = ports
            }, ActionSpecification.CancellationToken);
            
            throw new System.NotImplementedException();
        }

        private Dictionary<string, EmptyStruct> GetExposedPorts() => 
            _specifications.Ports.ToDictionary(x => $"{x.ExternalPort}:{x.InternalPort}", x => new EmptyStruct());
    }
}