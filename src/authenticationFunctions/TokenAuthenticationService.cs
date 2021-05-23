using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using BreakableLime.Authentication.models;
using BreakableLime.Authentication.models.credentials;
using BreakableLime.Authentication.models.specs;
using BreakableLime.Repository;
using BreakableLime.Repository.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BreakableLime.Authentication.Functions
{
    public class TokenAuthenticationService : ITokenAuthenticationService
    {
        private readonly TokenSigningCredentials _signingCredentials;
        private readonly TokenSpecification _verificationSpecification;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenAuthenticationService(TokenSigningCredentials signingCredentials, 
            TokenSpecification verificationSpecification, 
            ApplicationDbContext dbContext,
            ILogger logger,
            JwtSecurityTokenHandler tokenHandler = null)
        {
            _signingCredentials = signingCredentials ?? 
                                  throw new ArgumentNullException(nameof(signingCredentials));
            
            _verificationSpecification = verificationSpecification ?? 
                                         throw new ArgumentNullException(nameof(verificationSpecification));
            
            _dbContext = dbContext ?? 
                         throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;

            _tokenHandler = tokenHandler ?? new JwtSecurityTokenHandler();
        }


        public bool AuthenticateToken(string token, out ApplicationIdentityUser user)
        {
            _ = token ?? throw new ArgumentNullException(nameof(token));

            try
            {
                var claimsPrincipal = _tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _signingCredentials.SecurityKey,
                    ValidIssuer = _verificationSpecification.Issuer,
                    ValidAudience = _verificationSpecification.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);
                
                var tokenId = claimsPrincipal.Claims.First(x => x.Type == ApplicationClaimTypes.TokenId).Value;
                var unsafeUser = GetUser(tokenId) ?? throw new Exception($"{nameof(tokenId)} does not exist in db");

                user = unsafeUser;
                _logger.LogInformation("authenticated {User} by token", user.Id);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Invalid authentication attempt");

                user = null;
                return false;
            }
        }

        private ApplicationIdentityUser GetUser(string tokenId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.TokenId == tokenId);

            return user;
        }
    }
}