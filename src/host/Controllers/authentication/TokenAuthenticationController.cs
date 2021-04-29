using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BreakableLime.Host.Controllers.authentication
{
    [ApiController]
    [Route("authenticate/token")]
    public class TokenAuthenticationController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> Authenticate([FromBody] string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}