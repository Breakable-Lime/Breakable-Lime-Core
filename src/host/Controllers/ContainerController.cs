using System;
using System.Threading.Tasks;
using BreakableLime.ExternalModels.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Host.Controllers
{
    [Authorize]
    public class ContainerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContainerController> _logger;

        public ContainerController(IMediator mediator, ILogger<ContainerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        /// <summary>
        /// Start container in accordance with request specifications 
        /// </summary>
        /// <param name="request">The request specifying the new container</param>
        /// <returns>Created or BadRequest</returns>
        [Route("Start")]
        [HttpPost]
        public Task<IActionResult> StartContainer([FromBody]StartContainerRequest request)
        {
            throw new NotImplementedException(nameof(StartContainer));
        }

        /// <summary>
        /// Send stop to container and delete db-item
        /// </summary>
        /// <param name="containerId">The db entity id corresponding to a certain container</param>
        /// <returns>Ok or BadRequest</returns>
        [Route("Stop")]
        [HttpDelete]
        public Task<IActionResult> StopContainer([FromQuery]string containerId)
        {
            throw new NotImplementedException(nameof(StopContainer));
        }

        /// <summary>
        /// Gets the state of a specified container
        /// </summary>
        /// <param name="containerId">The db entity id corresponding to a certain container</param>
        /// <returns>Ok or BadRequest</returns>
        [Route("Status")]
        [HttpGet]
        public Task<IActionResult> FetchContainerStatus([FromQuery]string containerId)
        {
            throw new NotImplementedException(nameof(FetchContainerStatus));
        }

        /// <summary>
        /// Gets the logs from a container
        /// </summary>
        /// <param name="containerId">The db entity id corresponding to a certain container</param>
        /// <param name="numberOfLines">The amount of line to supply, if null then the max amount of line will be supplied</param>
        /// <returns>Ok or BadRequest</returns>
        [Route("Logs")]
        [HttpGet]
        public Task<IActionResult> FetchContainerLogs([FromQuery]string containerId, [FromQuery]int? numberOfLines = null)
        {
            throw new NotImplementedException(nameof(FetchContainerLogs));
        }
    }
}