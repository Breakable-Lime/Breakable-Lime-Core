using System;
using System.Linq;
using System.Threading.Tasks;
using BreakableLime.Authentication.models;
using BreakableLime.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Authentication.Functions
{
    public class CredentialAuthenticationService : ICredentialAuthenticationService
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;

        public CredentialAuthenticationService(UserManager<ApplicationIdentityUser> userManager, ILogger logger, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<ApplicationIdentityUser> Authenticate(UserCredentials credentials)
        {
            _ = credentials ?? throw new ArgumentNullException(nameof(credentials));

            var user = _dbContext.Users.FirstOrDefault(x => x.Email == credentials.Email);
            if (user == null)
            {
                _logger.LogError("Unable to authenticate {Email}", credentials.Email);
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, credentials.Password);

            if (result)
            {
                _logger.LogInformation("Authenticated {Email}", credentials.Email);
                return user;
            }
            
            _logger.LogError("Unable to authenticate {Email}", credentials.Email);
            return null;
        }
    }
}