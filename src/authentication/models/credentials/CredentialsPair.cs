namespace BreakableLime.Authentication.models.credentials
{
    public record CredentialsPair(ITokenSigningCredentials AuthenticationTokenCredentials,
        ITokenSigningCredentials RefreshTokenCredentials);
}