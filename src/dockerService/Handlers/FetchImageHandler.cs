using System;
using System.Threading.Tasks;
using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.DockerBackgroundService.models.external.ActionSpecifications;
using BreakableLime.GlobalModels;
using BreakableLime.GlobalModels.Wrappers;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Logging;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public class FetchImageHandler : DockerActionHandlerBase
    {
        private readonly ILogger<FetchImageHandler> _logger;

        private readonly FetchImageSpecification _specs;
        
        public FetchImageHandler(IDockerClient dockerClient, DockerWorkItem actionSpecification, ILogger<FetchImageHandler> logger) : base(dockerClient, actionSpecification)
        {
            _logger = logger;
            _specs = actionSpecification.SpecificationsMarker as FetchImageSpecification;
        }

        public override async Task Execute()
        {
            _logger.LogInformation("Fetching Image {Name}", _specs.ImageName);


            try
            {
                await DockerClient.Images.CreateImageAsync(new ImagesCreateParameters
                {
                    FromImage = _specs.ImageName,

                }, new AuthConfig(), new Progress<JSONMessage>(), ActionSpecification.CancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Image fetch failed {@Specifications}", _specs);
                _specs.ReturnStore.ReturnedValue = Result<Empty>.FromError<Empty>("Failed with exception; see log");
            }
            
            _logger.LogInformation("Successfully fetched image {@Specifications}", _specs);
            _specs.ReturnStore.ReturnedValue = Result<Empty>.FromResult(new Empty());

        }
    }
}