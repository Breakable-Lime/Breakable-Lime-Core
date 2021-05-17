using System;
using System.Threading.Tasks;
using BreakableLime.ExternalModels.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BreakableLime.Host.Controllers.authentication
{
    
    [ApiController]
    [Route("authenticate/password")]
    public class PasswordAuthenticateController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> Authenticate([FromBody] PasswordAuthenticateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}