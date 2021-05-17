using System.Collections.Generic;
using System.Security.Claims;
using BreakableLime.Authentication;

namespace BreakableLime.Repository.Models.Extentions
{
    public static class ApplicationIdentityUserClaimsCompute
    {
        public static IList<Claim> GetClaims(this ApplicationIdentityUser user)
        {
            var claims = new List<Claim>();

            var tokenIdClaim = new Claim(ApplicationClaimTypes.TokenId, user.TokenId);
            claims.Add(tokenIdClaim);

            return claims;
        }
    }
}