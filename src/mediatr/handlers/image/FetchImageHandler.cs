using System.Threading;
using System.Threading.Tasks;
using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.DockerBackgroundService.models.external.ActionSpecifications;
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
                
                //add to db
                    //using repo
                var imageEntity = new ContainerImage
                    {
                        Owner = await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == request.OwnerId,
                            cancellationToken),
                        RepositoryLocation = request.RepositoryUri
                    };
                    
                var result = await _imageService.CreateImage(imageEntity, cancellationToken);


                if (!result)
                {
                    _logger.LogError("Unable to create db-image {Id}", imageEntity.Id);
                    return Result<Empty>.FromError<Empty>("Unable to create db-item");
                }
                
                //add fetch image to queue
                    // use _dockerWorkQueue
                var dockerWorkItem = new DockerWorkItem
                    {
                        SpecificationsMarker = new FetchImageSpecification
                        {
                            ImageUri = request.RepositoryUri,
                            EntityId = imageEntity.Id
                        },
                        CancellationToken = cancellationToken
                    };

                _dockerWorkQueue.Enqueue(dockerWorkItem);



                //await fetched from server
                //try and wait

                var tries = 0;
                Result<Empty> dockerServResult;
                while (!((FetchImageSpecification) dockerWorkItem.SpecificationsMarker).ReturnStore.IsFinished(out dockerServResult) &&
                       !cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1500, cancellationToken);
                    tries++;

                    if (tries > 3)
                        break;    
                }
                
                //check if cancelled
                
                
                //return result
                if (dockerServResult.IsSuccessful)
                {
                 _logger.LogInformation("Successfully fetched image id {Id}", imageEntity.Id);
                 return Result<Empty>.FromResult<Empty>(new Empty());
                } 
                
                _logger.LogError("Docker Unable to fetch image with uri {@Uri}", request.RepositoryUri);
                return dockerServResult;
        }
    }
}