using System.Threading.Tasks;
using BreakableLime.Authentication.models;

namespace BreakableLime.Authentication.Functions
{
    public interface IRegistrationService
    {
        Task<bool> Register(ApplicationIdentityUser user, string password);
    }
}