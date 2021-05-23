using BreakableLime.DockerBackgroundService.models.external;
using Docker.DotNet;
using Microsoft.Extensions.Logging;

namespace BreakableLime.DockerBackgroundService.Handlers.Factories
{
    public class FetchImageHandlerFactory: DockerActionHandlerFactoryBase
    {
        private readonly ILoggerFactory _loggerFactory;


        public FetchImageHandlerFactory(DockerClient dockerClient, ILoggerFactory loggerFactory) : base(dockerClient)
        {
            _loggerFactory = loggerFactory;
            
        }

        public override IDockerActionHandler Create(DockerWorkItem actionSpecification)
        {
            var logger = _loggerFactory.CreateLogger<FetchImageHandler>(); 
            return new FetchImageHandler(DockerClient, actionSpecification, logger);
        }
    }
}