using System.Threading.Tasks;
using BreakableLime.Authentication.models;

namespace BreakableLime.Authentication.Functions
{
    public interface ICredentialAuthenticationService
    {
        Task<ApplicationIdentityUser> Authenticate(UserCredentials credentials);
    }
}