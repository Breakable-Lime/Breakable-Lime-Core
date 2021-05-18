using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.ExternalModels.Requests;
using BreakableLime.GlobalModels;
using BreakableLime.GlobalModels.Wrappers;
using BreakableLime.Mediatr.requests.user;
using BreakableLime.Repository;
using BreakableLime.Repository.Models;
using BreakableLime.Repository.Models.Extentions;
using BreakableLime.Repository.services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FetchImageRequest = BreakableLime.Mediatr.requests.image.FetchImageRequest;

namespace BreakableLime.Mediatr.handlers.image
{
    public class FetchImageHandler : IRequestHandler<FetchImageRequest, Result<Empty>>
    {
        private readonly ILogger<FetchImageHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IDockerWorkQueue _dockerWorkQueue;
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imageService;

        public FetchImageHandler(ILogger<FetchImageHandler> logger, 
            IMediator mediator, 
            IDockerWorkQueue dockerWorkQueue, 
            ApplicationDbContext dbContext,
            IImageService imageService)
        {
            _logger = logger;
            _mediator = mediator;
            _dockerWorkQueue = dockerWorkQueue;
            _dbContext = dbContext;
            _imageService = imageService;
        }
        
        public async Task<Result<Empty>> Handle(FetchImageRequest request, CancellationToken cancellationToken)
        {
            //check allocation is allowed
                //send to mediator to fetch with db context
                //check allocation with extension method
                
                //gets user
                var user = await _mediator.Send(new UserByIdRequest {UserId = request.OwnerId}, cancellationToken);
            
                //if !bad cont
                if (!user.IsSuccessful)
                {
                    _logger.LogError("Unable to fetch image due to invalid user id {Id}", request.OwnerId);
                    return Result<Empty>.FromError<Empty>("Unable to fetch image due to invalid user id: see log");
                }
                
                //does conform
                if (!user.Product.IsConformingToIncrementedImageAllocation())
                {
                    _logger.LogError("Unable to fetch image due to user having max allocation of {@Allocation}", user.Product.ImageAllocation);
                    return Result<Empty>.FromError<Empty>("Unable to fetch image due to the user using their max image allocation");
                }


                //add fetch image to queue
                // use _dockerWorkQueue
            
            //add to db
                //using repo
                var result = await _imageService.CreateImage(new ContainerImage
                {
                 Owner   = await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == request.OwnerId, cancellationToken),
                 ImageNativeId = "", //TODO: perhaps through a quick httpclient request
                 RepositoryLocation = request.RepositoryUri
                }, cancellationToken);
                
            //await fetched from server
                //try and wait
                
            //return result
            
            
            throw new System.NotImplementedException();
        }
    }
}