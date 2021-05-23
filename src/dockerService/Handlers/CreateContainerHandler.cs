using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.DockerBackgroundService.models.external.ActionSpecifications;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Logging;

namespace BreakableLime.DockerBackgroundService.Handlers
{
    public class CreateContainerHandler : DockerActionHandlerBase
    {
        private readonly ILogger<CreateContainerHandler> _logger;
        private readonly CreateContainerSpecification _specifications;

        public CreateContainerHandler(IDockerClient dockerClient, DockerWorkItem actionSpecification, ILogger<CreateContainerHandler> logger) : base(
            dockerClient, actionSpecification)
        {
            _logger = logger;
            _specifications = actionSpecification.SpecificationsMarker as CreateContainerSpecification;
        }

        public override async Task Execute()
        {
            var ports = GetExposedPorts();

            try
            {
                var response = await DockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
                {
                    Image = _specifications.ImageHash,
                    ExposedPorts = ports
                }, ActionSpecification.CancellationToken);

                if (response.Warnings == null || !response.Warnings.Any())
                {
                    _logger.LogInformation("Created container with id {Id}", response.ID);
                    _specifications.ReturnStore.ReturnedValue = true;
                }

                _logger.LogError("Unable to Create Container with specs {@Specs}", _specifications);
                _specifications.ReturnStore.ReturnedValue = false;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unable to Create Container with spec {@Spec} and exception was thrown", _specifications);
                _specifications.ReturnStore.ReturnedValue = false;
            }
            
        }

        private Dictionary<string, EmptyStruct> GetExposedPorts() => 
            _specifications.Ports.ToDictionary(x => $"{x.ExternalPort}:{x.InternalPort}", x => new EmptyStruct());
    }
}