using System.Threading.Tasks;
using BreakableLime.Authentication.models;
using BreakableLime.Repository.Models;

namespace BreakableLime.Authentication.Functions
{
    public interface IRegistrationService
    {
        Task<bool> Register(ApplicationIdentityUser user, string password);
    }
}