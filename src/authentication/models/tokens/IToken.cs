using System;

namespace BreakableLime.Authentication.models.tokens
{
    public interface IToken
    {
        public string TokenId { get; init; }
        public string Token { get; init; }
        public DateTime Expiration { get; init; }
    }
}