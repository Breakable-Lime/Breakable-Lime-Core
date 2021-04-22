using BreakableLime.DockerBackgroundService.models.external;

namespace BreakableLime.DockerBackgroundService.Handlers.Factories
{
    public interface IDockerActionHandlerFactory
    {
        public IDockerActionHandler Create(DockerWorkItem actionSpecification);
    }
}