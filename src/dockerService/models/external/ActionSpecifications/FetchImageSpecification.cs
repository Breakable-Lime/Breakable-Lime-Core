using System;

namespace BreakableLime.DockerBackgroundService.models.external.ActionSpecifications
{
    public record FetchImageSpecification : DockerWorkSpecificationMarker
    {
        public string EntityId;
        public Uri ImageUri;
    }
}