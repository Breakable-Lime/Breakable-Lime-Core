using System.Threading.Tasks;
using BreakableLime.Authentication.models;
using BreakableLime.Repository.Models;

namespace BreakableLime.Authentication.Functions
{
    public interface ICredentialAuthenticationService
    {
        Task<ApplicationIdentityUser> Authenticate(UserCredentials credentials);
    }
}