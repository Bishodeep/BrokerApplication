using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using clean.Infrastructure.Models;
using clean.Application.Contracts.Services;
using Microsoft.Extensions.Options;

namespace clean.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public TokenService(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateEncodedToken(List<Claim> claims)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtOptions.ExpireDays));

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                null,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
