using System;
using System.Threading;

namespace BreakableLime.DockerBackgroundService.models.external
{
    public abstract record DockerWorkItem
    {
        public DockerWorkSpecificationMarker SpecificationsMarker { get; init; }
        public CancellationToken CancellationToken { get; init; } 
    }

}


