namespace BreakableLime.Authentication.models.tokens
{
    public record TokenPair
    {
        public AuthenticationToken AuthenticationToken;
        public RefreshToken RefreshToken;
    }
}