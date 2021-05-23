using BreakableLime.DockerBackgroundService.models.external;
using Docker.DotNet;
using Microsoft.Extensions.Logging;

namespace BreakableLime.DockerBackgroundService.Handlers.Factories
{
    public class DeleteImageHandlerFactory : DockerActionHandlerFactoryBase
    {
        private readonly ILoggerFactory _loggerFactory;

        public DeleteImageHandlerFactory(DockerClient dockerClient, ILoggerFactory loggerFactory) : base(dockerClient)
        {
            _loggerFactory = loggerFactory;
        }

        public override IDockerActionHandler Create(DockerWorkItem actionSpecification)
        {
            var logger = _loggerFactory.CreateLogger<DeleteImageHandler>();
            return new DeleteImageHandler(DockerClient, actionSpecification, logger);
        }
    }
}