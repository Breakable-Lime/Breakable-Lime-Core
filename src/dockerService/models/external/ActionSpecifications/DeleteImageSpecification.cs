using BreakableLime.GlobalModels;
using BreakableLime.GlobalModels.Wrappers;

namespace BreakableLime.DockerBackgroundService.models.external.ActionSpecifications
{
    public record DeleteImageSpecification : DockerWorkSpecificationMarker
    {
        public string ImageId;
        
        public readonly DockerReturnStore<Result<Empty>> ReturnStore = new DockerReturnStore<Result<Empty>>();
    }
}