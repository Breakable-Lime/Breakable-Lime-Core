using System;

namespace BreakableLime.Authentication.models.specs
{
    public record RefreshTokenSpecification : TokenSpecificationBase
    {
        public string RefreshHttpMethod { get; init; }
        public string RefreshHttpUrl { get; init; }
        
        public RefreshTokenSpecification(string issuer, string audience, TimeSpan validityPeriod, string refreshHttpMethod, string refreshHttpUrl) 
            : base(issuer, audience, validityPeriod)
        {
            RefreshHttpMethod = refreshHttpMethod;
            RefreshHttpUrl = refreshHttpUrl;
        }
    }
}