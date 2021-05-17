using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BreakableLime.Host.Controllers.authentication
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> CreateUser()
        {
            throw new NotImplementedException();
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