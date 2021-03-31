using System;

namespace BreakableLime.Authentication.models.tokens
{
    public record RefreshToken : IToken
    {
        public string TokenId { get; init; }
        public string Token { get; init; }
        public DateTime Expiration { get; init; }
        
        public string RefreshHttpMethod { get; init; }
        public string RefreshHttpUrl { get; init; }
    }
}