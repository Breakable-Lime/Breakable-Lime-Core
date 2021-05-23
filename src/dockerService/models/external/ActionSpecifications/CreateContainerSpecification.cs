using System.Collections.Generic;

namespace BreakableLime.DockerBackgroundService.models.external.ActionSpecifications
{
    public record CreateContainerSpecification : DockerWorkSpecificationMarker
    {
        public string ImageHash { get; init; }
        public IList<ExposedPort> Ports { get; init; }
        public DockerReturnStore<bool> ReturnStore { get; init; } = new DockerReturnStore<bool>();
    }
}