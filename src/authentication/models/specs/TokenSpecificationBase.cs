using System;

namespace BreakableLime.Authentication.models.specs
{
    public abstract record TokenSpecificationBase : ITokenSpecification
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public TimeSpan ValidityPeriod { get; init; }

        protected TokenSpecificationBase(string issuer, string audience, TimeSpan validityPeriod)
        {
            Issuer = issuer;
            Audience = audience;
            ValidityPeriod = validityPeriod;
        }

    }
}