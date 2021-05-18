using System;
using System.Reflection.Metadata;
using BreakableLime.GlobalModels;
using BreakableLime.GlobalModels.Wrappers;

namespace BreakableLime.DockerBackgroundService.models.external.ActionSpecifications
{
    public record FetchImageSpecification : DockerWorkSpecificationMarker
    {
        public string EntityId;
        public Uri ImageUri;

        public readonly DockerReturnStore<Result<Empty>> ReturnStore = new DockerReturnStore<Result<Empty>>();
    }
}