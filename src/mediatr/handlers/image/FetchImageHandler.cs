using System.Threading;
using System.Threading.Tasks;
using BreakableLime.DockerBackgroundService.models.external;
using BreakableLime.ExternalModels.Requests;
using BreakableLime.GlobalModels;
using BreakableLime.GlobalModels.Wrappers;
using BreakableLime.Mediatr.requests.user;
using BreakableLime.Repository;
using MediatR;
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

        public FetchImageHandler(ILogger<FetchImageHandler> logger, 
            IMediator mediator, 
            IDockerWorkQueue dockerWorkQueue, 
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _mediator = mediator;
            _dockerWorkQueue = dockerWorkQueue;
            _dbContext = dbContext;
        }
        
        public async Task<Result<Empty>> Handle(FetchImageRequest request, CancellationToken cancellationToken)
        {
            //check allocation is allowed
                //send to mediator to fetch with db context
                //check allocation with extension method

                var user = await _mediator.Send(new UserByIdRequest {UserId = request.OwnerId}, cancellationToken);
            
            //add fetch image to queue
                // use _dockerWorkQueue
            
            //add to db
                //using repo
                
            //await fetched from server
                //try and wait
                
            //return result
            
            
            throw new System.NotImplementedException();
        }
    }
}