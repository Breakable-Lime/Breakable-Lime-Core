using BreakableLime.Authentication.models;
using BreakableLime.Repository.Models;

namespace BreakableLime.Authentication.Functions
{
    public interface ITokenAuthenticationService
    {
        bool AuthenticateToken(string token, out ApplicationIdentityUser user);
    }
}