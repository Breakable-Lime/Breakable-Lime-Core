using System;

namespace BreakableLime.DockerBackgroundService.models.external
{
    public interface IDockerWorkItem<T> where T : IDockerWorkSpecification
    {
        public DockerWorkType DockerWorkType { get; init; }
        public T Specifications { get; init; }
    }

}


