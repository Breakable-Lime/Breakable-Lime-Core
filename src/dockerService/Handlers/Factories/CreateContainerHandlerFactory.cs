using BreakableLime.DockerBackgroundService.models.external;
using Docker.DotNet;
using Microsoft.Extensions.Logging;

namespace BreakableLime.DockerBackgroundService.Handlers.Factories
{
    public class CreateContainerHandlerFactory : DockerActionHandlerFactoryBase
    {
        private readonly ILoggerFactory _loggerFactory;

        public CreateContainerHandlerFactory(DockerClient dockerClient, ILoggerFactory loggerFactory) : base(dockerClient)
        {
            _loggerFactory = loggerFactory;
        }

        public override IDockerActionHandler Create(DockerWorkItem actionSpecification)
        {
            var logger = _loggerFactory.CreateLogger<CreateContainerHandler>();
            return new CreateContainerHandler(DockerClient, actionSpecification, logger);
        }
    }
}