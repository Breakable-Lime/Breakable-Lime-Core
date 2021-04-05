using System;

namespace BreakableLime.Authentication.models.specs
{
    public record AuthenticationTokenSpecification : TokenSpecification
    {
        public AuthenticationTokenSpecification(string issuer, string audience, TimeSpan validityPeriod) 
            : base(issuer, audience, validityPeriod)
        {
            
        }
    }
}