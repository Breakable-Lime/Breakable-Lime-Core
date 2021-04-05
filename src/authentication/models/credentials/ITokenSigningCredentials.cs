using Microsoft.IdentityModel.Tokens;

namespace BreakableLime.Authentication.models.credentials
{
    public interface ITokenSigningCredentials
    {
        SigningCredentials SigningCredentials { get; init; }
    }
}