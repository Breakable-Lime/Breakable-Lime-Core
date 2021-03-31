using System.Collections.Generic;
using System.Security.Claims;
using BreakableLime.Authentication.models.tokens;

namespace BreakableLime.Authentication.factories
{
    public interface ITokenFactory
    {
        //new (Signing Credentials, GeneralTokenSettings)

        public IToken CreateToken(IList<Claim> claims);
    }
}