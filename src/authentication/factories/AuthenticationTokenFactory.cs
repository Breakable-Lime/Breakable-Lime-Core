using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using BreakableLime.Authentication.models.credentials;
using BreakableLime.Authentication.models.specs;
using BreakableLime.Authentication.models.tokens;
using Microsoft.IdentityModel.Tokens;

namespace BreakableLime.Authentication.factories
{
    public class AuthenticationTokenFactory : TokenFactory
    {


        
        public AuthenticationTokenFactory(ITokenSigningCredentials credentials,
            ITokenSpecification specs, JwtSecurityTokenHandler tokenHandler = null) 
                : base(credentials, specs, tokenHandler)
        {

        }
        
        
        public override AuthenticationToken CreateToken(IList<Claim> claims)
        {
            _ = claims ?? throw new ArgumentNullException(nameof(claims));
            if (!claims.Any())
                throw new ArgumentOutOfRangeException(nameof(claims), "no contents");

            var specs = CreateSpecification(claims);
            var tokenString = JwtSecurityTokenHandler.WriteToken(specs);

            var token = new AuthenticationToken
            {
                TokenId = claims.First(x => x.Type == ApplicationClaimTypes.TokenId).Value,
                Token = tokenString,
                Expiration = specs.ValidTo
            };
            
            return token;
        }

       
    }
}