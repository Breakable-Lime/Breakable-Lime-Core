using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BreakableLime.Authentication.models.credentials;
using BreakableLime.Authentication.models.specs;
using BreakableLime.Authentication.models.tokens;

namespace BreakableLime.Authentication.factories
{
    public abstract class TokenFactory : ITokenFactory
    {
        protected readonly ITokenSigningCredentials Credentials;
        protected readonly ITokenSpecification Specification;
        protected readonly JwtSecurityTokenHandler JwtSecurityTokenHandler;

        protected TokenFactory(ITokenSigningCredentials credentials, ITokenSpecification specification,
            JwtSecurityTokenHandler jwtSecurityTokenHandler = null)
        {
            _ = credentials ?? throw new ArgumentNullException(nameof(credentials));
            Credentials = credentials;

            _ = specification ?? throw new ArgumentNullException(nameof(specification));
            Specification = specification;
            
            
            JwtSecurityTokenHandler = jwtSecurityTokenHandler ?? new JwtSecurityTokenHandler();
        }

        public abstract IToken CreateToken(IList<Claim> claims);
        
        protected virtual JwtSecurityToken CreateSpecification(IEnumerable<Claim> claims) =>
            new JwtSecurityToken(
                issuer: this.Specification.Issuer, 
                audience: Specification.Audience, 
                claims: claims, 
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(Specification.ValidityPeriod),
                signingCredentials: Credentials.SigningCredentials);

    }
}