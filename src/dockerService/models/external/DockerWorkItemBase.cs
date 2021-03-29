using System;
using System.Threading;

namespace BreakableLime.DockerBackgroundService.models.external
{
    public abstract record DockerWorkItemBase
    {
        public DockerWorkType DockerWorkType { get; init; }
        public IDockerWorkSpecification Specifications { get; init; }
        public CancellationToken CancellationToken { get; init; } 
    }

}


