using BreakableLime.Authentication.models.tokens;
using BreakableLime.GlobalModels.Wrappers;
using MediatR;

namespace BreakableLime.Mediatr.requests.authentication
{
    public class PasswordAuthenticationRequest : IRequest<Result<TokenPair>>
    {
        public string Email { get; set; } 
        public string Password { get; set; }
    }
}