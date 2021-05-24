using System;
using System.Threading.Tasks;
using BreakableLime.ExternalModels.Requests;
using BreakableLime.Mediatr.requests.user;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BreakableLime.Host.Controllers.authentication
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        
        /// <summary>
        /// Creates a user in accordance with the body
        /// </summary>
        /// <param name="request">The body specifying the specifications to create the user with</param>
        /// <returns>Created with the location or empty BadRequest</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
            
            //mediatr
            var result = await _mediator.Send(new RegisterUserRequest
            {
                Email = request.Email,
                Class = request.Class,
                Password = request.Password,
                IsAdmin = request.IsAdmin
            });
            
            //validate 
            if (!result.IsSuccessful)
            {
                _logger.LogError("Unable to register the user with email {Email}", request.Email);
                return BadRequest();
            }
            
            //return Created
            _logger.LogInformation("Successfully registered a user with id {Id}", result.Product.Id);
            return Created(this.HttpContext.Request.PathBase + $"/user/id={result.Product.Id}", result.Product); //TODO: do not return real user!!!
        }

        [HttpGet]
        public Task<IActionResult> GetUser()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public Task<IActionResult> DeleteUser()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public Task<IActionResult> EditUser()
        {
            throw new NotImplementedException();
        }
    }
}