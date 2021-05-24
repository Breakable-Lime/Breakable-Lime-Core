using BreakableLime.GlobalModels.Wrappers;
using BreakableLime.Repository.Models;
using MediatR;

namespace BreakableLime.Mediatr.requests.user
{
    public class RegisterUserRequest : IRequest<Result<ApplicationIdentityUser>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Class { get; set; }

        public bool IsAdmin { get; set; }
    }
}