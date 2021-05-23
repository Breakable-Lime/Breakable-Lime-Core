using System;
using System.Linq;
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
    public class DeleteImageHandler : DockerActionHandlerBase
    {
        private readonly ILogger<DeleteImageHandler> _logger;
        private readonly DeleteImageSpecification _spec;

        public DeleteImageHandler(IDockerClient dockerClient, 
            DockerWorkItem actionSpecification, 
            ILogger<DeleteImageHandler> logger) : base(dockerClient, actionSpecification)
        {
            _logger = logger;
            _spec = actionSpecification.SpecificationsMarker as DeleteImageSpecification;
        }

        public override async Task Execute()
        {
            try
            {
                var result = await DockerClient.Images.DeleteImageAsync(_spec.ImageId, new ImageDeleteParameters(),
                    ActionSpecification.CancellationToken);

                if (result.Any())
                {
                    _logger.LogInformation("Successfully deleted image {Id}", _spec.ImageId);
                    _spec.ReturnStore.ReturnedValue = Result<Empty>.FromResult<Empty>(new Empty());
                }
                else
                {
                    _logger.LogError("Unable to delete image {Id}", _spec.ImageId);
                    _spec.ReturnStore.ReturnedValue = Result<Empty>.FromError<Empty>("Unable to delete image");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unable to delete image {Id}, exception was thrown", _spec.ImageId);
                _spec.ReturnStore.ReturnedValue = Result<Empty>.FromError<Empty>("Unable to delete image, exception was thrown");
                return;
            }
        }
    }
}