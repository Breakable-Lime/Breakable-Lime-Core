using System;
using System.Threading.Tasks;
using BreakableLime.Authentication.models;
using BreakableLime.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Authentication.Functions
{


    public class RegistrationService : IRegistrationService
    {
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public RegistrationService(ILogger logger, UserManager<ApplicationIdentityUser>  userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<bool> Register(ApplicationIdentityUser user, string password)
        {
            
            //check user
            _ = user ?? throw new ArgumentNullException(nameof(user));
            
            //check password
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentOutOfRangeException(nameof(password));
            
            //try register

            var result = await _userManager.CreateAsync(user, password);

            //check result
            if (!result.Succeeded)
            {
                _logger.LogWarning("Unable to register user with error {error}", result.Errors);
                return false;
            }
            else
            {
                _logger.LogInformation("Registered user {ID}", user.Id);
                return true;
            }
        }
    }
}