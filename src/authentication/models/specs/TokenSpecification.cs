using System;

namespace BreakableLime.Authentication.models.specs
{
    public record TokenSpecification : ITokenSpecification
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public TimeSpan ValidityPeriod { get; init; }

        protected TokenSpecification(string issuer, string audience, TimeSpan validityPeriod)
        {
            Issuer = issuer;
            Audience = audience;
            ValidityPeriod = validityPeriod;
        }

    }
}