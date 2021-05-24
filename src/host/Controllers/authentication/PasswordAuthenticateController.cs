using System;
using System.Globalization;
using System.Threading.Tasks;
using BreakableLime.ExternalModels.Requests;
using BreakableLime.ExternalModels.responses.TokenPair;
using BreakableLime.Mediatr.handlers.authentication;
using BreakableLime.Mediatr.requests.authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Host.Controllers.authentication
{
    
    [ApiController]
    [Route("authenticate/password")]
    public class PasswordAuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PasswordAuthenticateController> _logger;

        public PasswordAuthenticateController(IMediator mediator, ILogger<PasswordAuthenticateController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        /// <summary>
        /// Creates bearer token pair if request is validated
        /// </summary>
        /// <param name="request">Request consisting of password and email used to authenticate a user</param>
        /// <returns>Ok with token pair or BadRequest</returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] PasswordAuthenticateRequest request)
        {
            //make and send request
            try
            {
                var tokenPair = await _mediator.Send(new PasswordAuthenticationRequest()
                {
                    Email = request.Email,
                    Password = request.Password
                });

                if (!tokenPair.IsSuccessful)
                {
                    _logger.LogError("Unable to authenticate");
                    return BadRequest();
                }
                
                //create new item if result valid
                var output = new TokenPair
                {
                    AuthenticationToken = new ClientToken
                    {
                        Token = tokenPair.Product.AuthenticationToken.Token,
                        TokenIsoExpiry = tokenPair.Product.AuthenticationToken.Expiration.ToString(CultureInfo.InvariantCulture)
                    },
                    RefreshToken = new ClientToken
                    {
                        Token = tokenPair.Product.RefreshToken.Token,
                        TokenIsoExpiry = tokenPair.Product.RefreshToken.Expiration.ToString(CultureInfo.InvariantCulture)
                    }
                };

                //return

                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Bad Authentication Request caused handler to throw");
                return BadRequest();
            }
        }
    }
}