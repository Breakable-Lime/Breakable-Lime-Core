using System.Threading;
using System.Threading.Tasks;
using BreakableLime.GlobalModels.Wrappers;
using BreakableLime.Mediatr.requests.user;
using BreakableLime.Repository;
using BreakableLime.Repository.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Mediatr.handlers.user
{
    public class UserByIdHandler : IRequestHandler<UserByIdRequest, Result<ApplicationIdentityUser>>
    {
        private readonly ILogger<UserByIdHandler> _logger;
        private readonly ApplicationDbContext _dbContext;

        public UserByIdHandler(ILogger<UserByIdHandler> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task<Result<ApplicationIdentityUser>> Handle(UserByIdRequest request, CancellationToken cancellationToken)
        {
            //fetch user
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            
            //if user != null
            if (user != null)
            {
                _logger.LogInformation("Fetched User {UserId} from db", user.Id);
                return Result<ApplicationIdentityUser>.FromResult(user);
            }
            
            _logger.LogError("Unable to fetch user {UserId}", request.UserId);
            return Result<ApplicationIdentityUser>.FromError<ApplicationIdentityUser>("Unable to recover user");
        }
    }
}