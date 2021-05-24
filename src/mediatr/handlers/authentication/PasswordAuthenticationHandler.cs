using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BreakableLime.Authentication;
using BreakableLime.Authentication.factories;
using BreakableLime.Authentication.Functions;
using BreakableLime.Authentication.models;
using BreakableLime.Authentication.models.tokens;
using BreakableLime.GlobalModels.Wrappers;
using BreakableLime.Mediatr.requests.authentication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Mediatr.handlers.authentication
{
    public class PasswordAuthenticationHandler : IRequestHandler<PasswordAuthenticationRequest, Result<TokenPair>>
    {
        private readonly ILogger<PasswordAuthenticationHandler> _logger;
        private readonly AuthenticationTokenFactory _authenticationTokenFactory;
        private readonly RefreshTokenFactory _refreshTokenFactory;
        private readonly ICredentialAuthenticationService _credentialAuthenticationService;

        public PasswordAuthenticationHandler(ILogger<PasswordAuthenticationHandler> logger, 
            AuthenticationTokenFactory authenticationTokenFactory, 
            RefreshTokenFactory refreshTokenFactory,
            ICredentialAuthenticationService credentialAuthenticationService)
        {
            _logger = logger;
            _authenticationTokenFactory = authenticationTokenFactory;
            _refreshTokenFactory = refreshTokenFactory;
            _credentialAuthenticationService = credentialAuthenticationService;
        }
        
        public async Task<Result<TokenPair>> Handle(PasswordAuthenticationRequest request, CancellationToken cancellationToken)
        {
            //check request
            _ = request.Email ?? throw new ArgumentNullException(request.Email);
            _ = request.Password ?? throw new ArgumentNullException(request.Password);

            //get user
            //check valid
            var user =
                await _credentialAuthenticationService.Authenticate(new UserCredentials(request.Email, request.Password));

            if (user == null)
            {
                _logger.LogError("No user with email {Email} and password x", request.Email);
                return Result<TokenPair>.FromError<TokenPair>("Unable to authenticate");
            }

            //create tokens
            var authToken = _authenticationTokenFactory.CreateToken(new List<Claim>
            {
                new Claim(ApplicationClaimTypes.TokenId, user.TokenId)
            });

            var refreshToken = _refreshTokenFactory.CreateToken(new List<Claim>
            {
                new Claim(ApplicationClaimTypes.TokenId, user.TokenId)
            });
            
            //validate
            if (authToken == null || refreshToken == null)
            {
                _logger.LogError("Unable to create all tokens");
                return Result<TokenPair>.FromError<TokenPair>("Unable to create all tokens");
            }

            var tokenPair = new TokenPair
            {
                AuthenticationToken = authToken,
                RefreshToken = refreshToken
            };

            //return
            return Result<TokenPair>.FromResult(tokenPair);
        }
    }
}