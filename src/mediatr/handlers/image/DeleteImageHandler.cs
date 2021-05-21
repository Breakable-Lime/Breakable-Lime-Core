using System;
using System.Threading;
using System.Threading.Tasks;
using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.DockerBackgroundService.models.external.ActionSpecifications;
using BreakableLime.GlobalModels;
using BreakableLime.GlobalModels.Wrappers;
using BreakableLime.Mediatr.requests.image;
using BreakableLime.Repository;
using BreakableLime.Repository.services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Mediatr.handlers.image
{
    public class DeleteImageHandler : IRequestHandler<DeleteImageRequest, Result<Empty>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imageService;
        private readonly IMediator _mediator;
        private readonly ILogger<DeleteImageHandler> _logger;

        public DeleteImageHandler(ApplicationDbContext dbContext, 
            IImageService imageService, 
            IMediator mediator, 
            ILogger<DeleteImageHandler> logger)
        {
            _dbContext = dbContext;
            _imageService = imageService;
            _mediator = mediator;
            _logger = logger;
        }

        
        
        public async Task<Result<Empty>> Handle(DeleteImageRequest request, CancellationToken cancellationToken)
        {
            //check image exists
            var image = await _dbContext.Images.FirstOrDefaultAsync(c => c.Id == request.EntityId, cancellationToken);
            var imageExists = image != null;

            if (!imageExists)
            {
                _logger.LogError("Not able to delete non existent image {Id}", request.EntityId);
                return Result<Empty>.FromError<Empty>("Not able to delete non existent image");
            }
            
            //delete image
            var dockerRequest = new DockerWorkItem
            {
                SpecificationsMarker = new DeleteImageSpecification
                {
                    ImageId = image.Id
                },
                CancellationToken = cancellationToken
            };
            
            //await docker
            var tries = 0;
            Result<Empty> result;
            while (!((DeleteImageSpecification) (dockerRequest.SpecificationsMarker)).ReturnStore.IsFinished(
                out result) || cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cancellationToken);
                tries++;
                if (tries >= 3)
                    break;
            }
            
            //check docker result
            if (!result.IsSuccessful)
            {
                _logger.LogError("Unable to delete stored image {Id}", image.Id);
                return Result<Empty>.FromError<Empty>("Unable to delete store image");
            }

            
            //delete image entity
            var entityResult = await _imageService.DeleteImage(image, cancellationToken);
            
            //check cancellation
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogError("cancellation requested; status of image {Id} unknown", image.Id);
                return Result<Empty>.FromError<Empty>("cancellation requested; image status unsure");
            }
            
            //verify & return

            if (entityResult)
            {
                _logger.LogInformation("Successfully deleted image {Id}", image.Id);
                return Result<Empty>.FromResult(new Empty());
            }
            
            _logger.LogError("Deletion of image {Id} failed", image.Id);
            return Result<Empty>.FromError<Empty>("Unable to delete image");
        }
    }
}