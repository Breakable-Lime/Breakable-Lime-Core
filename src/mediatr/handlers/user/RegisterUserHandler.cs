using System;
using System.Threading;
using System.Threading.Tasks;
using BreakableLime.Authentication.Functions;
using BreakableLime.GlobalModels.Wrappers;
using BreakableLime.Mediatr.requests.user;
using BreakableLime.Repository;
using BreakableLime.Repository.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Mediatr.handlers.user
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Result<ApplicationIdentityUser>>
    {
        private readonly ILogger<RegisterUserHandler> _logger;
        private readonly IRegistrationService _registrationService;
        private readonly ApplicationDbContext _applicationDbContext;

        public RegisterUserHandler(ILogger<RegisterUserHandler> logger, IRegistrationService registrationService, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _registrationService = registrationService;
            _applicationDbContext = applicationDbContext;
        }
        
        public async Task<Result<ApplicationIdentityUser>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            //check is good
            _ = request.Email ?? throw new ArgumentNullException(request.Email);
            _ = request.Password ?? throw new ArgumentNullException(request.Password);
            
            //make user
            var user = new ApplicationIdentityUser
            {
                Email = request.Email,
                Class = request.Class,
            };

            var result = await _registrationService.Register(user, request.Password);

            if (!result)
            {
                _logger.LogError("Unable to create user with email {Email}", request.Email);
                return Result<ApplicationIdentityUser>.FromError<ApplicationIdentityUser>("Unable to create user");
            }

            var savedUser =
                await _applicationDbContext.Users.FirstOrDefaultAsync(c => c.Email == user.Email, cancellationToken);

            if (savedUser == null)
            {
                _logger.LogError("Unable to fetch \"Created\" user with email {Email}", request.Email);
                return Result<ApplicationIdentityUser>.FromError<ApplicationIdentityUser>("Unable to fetch user");
            }
            
            //return

            return Result<ApplicationIdentityUser>.FromResult(savedUser);
        }
    }
}