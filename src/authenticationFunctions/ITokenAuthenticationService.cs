using BreakableLime.Authentication.models;

namespace BreakableLime.Authentication.Functions
{
    public interface ITokenAuthenticationService
    {
        bool AuthenticateToken(string token, out ApplicationIdentityUser user);
    }
}