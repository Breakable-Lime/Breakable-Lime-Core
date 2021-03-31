using System;

namespace BreakableLime.Authentication.models.specs
{
    public interface ITokenSpecification
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public TimeSpan ValidityPeriod { get; init; }
    }
}