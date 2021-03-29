using System;

namespace BreakableLime.Authentication.models.tokens
{
    public record AuthenticationToken : IToken
    {
        public string TokenId { get; init; }
        public string Token { get; init; }
        public DateTime Expiration { get; init; }

        public string Header => $"Bearer {Token}";
    }
}