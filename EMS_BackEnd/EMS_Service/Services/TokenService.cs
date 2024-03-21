using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EMS_Domain.Model;
using EMS_Service.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace EMS_Service.Services
{
	public class TokenService : ITokenService
	{
        public async Task<string> GenerateAccessToken(JWTSettings _jwtKEY, IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKEY.Secret));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var _TokenExpiryTimeInMinutes = Convert.ToInt64(_jwtKEY.TokenExpiryTimeInMinutes);

            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtKEY.ValidIssuer,
                audience: _jwtKEY.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_TokenExpiryTimeInMinutes),
                signingCredentials: signinCredentials
            );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokeOptions));
        }
        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return await Task.FromResult(Convert.ToBase64String(randomNumber));
            }
        }
        public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(JWTSettings _jwtKEY, string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKEY.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return await Task.FromResult(principal);
        }
    }
}

