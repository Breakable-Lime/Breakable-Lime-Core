using System;
using System.Diagnostics.SymbolStore;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BreakableLime.Authentication.models.credentials
{
    public class TokenSigningCredentials : ITokenSigningCredentials
    {
        public SigningCredentials SigningCredentials { get; init; }
        public SymmetricSecurityKey SecurityKey { get; init; }
        

        public TokenSigningCredentials(string secret)
        {
            var key = GetSecurityKey(secret);
            SecurityKey = key;
            
            var credentials = CreateSigningCredentials(key);

            SigningCredentials = credentials;
        }

        private SymmetricSecurityKey GetSecurityKey(string secret)
        {
            _ = secret ?? throw new ArgumentException($"{nameof(secret)} is null");
            
            var bytes = Encoding.UTF8.GetBytes(secret);
            var key = new SymmetricSecurityKey(bytes);

            return key;
        }

        private SigningCredentials CreateSigningCredentials(SymmetricSecurityKey key) =>
            new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
}