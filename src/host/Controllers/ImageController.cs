using System;
using System.Threading.Tasks;
using BreakableLime.ExternalModels.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Host.Controllers
{
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IMediator mediator, ILogger<ImageController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Calling this endpoint will fetch an image from a repository and add a db item representing the image
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Created or BadRequest</returns>
        [Route("Fetch")]
        [HttpPost]
        public async Task<IActionResult> FetchImage([FromBody]FetchImageRequest request)
        {
            throw new NotImplementedException(nameof(FetchImage));
        }

        /// <summary>
        /// Calling this endpoint deletes an image from the db and store using the image id provided
        /// </summary>
        /// <param name="imageId">the database identifier for the item representing the image</param>
        /// <returns>Ok or BadRequest</returns>
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromQuery] string imageId)
        {
            throw new NotImplementedException(nameof(DeleteImage));
        }
    }
}