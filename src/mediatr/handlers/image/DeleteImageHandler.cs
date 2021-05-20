using System;
using System.Threading;
using System.Threading.Tasks;
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
            
            
            //delete image entity
            var entityResult = await _imageService.DeleteImage(image, cancellationToken);
            
            //verify & return
            
            throw new System.NotImplementedException();
        }
    }
}