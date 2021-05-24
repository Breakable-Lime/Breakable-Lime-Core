namespace BreakableLime.ExternalModels.responses.TokenPair
{
    public record TokenPair
    {
        public ClientToken AuthenticationToken;
        public ClientToken RefreshToken;
    }
}