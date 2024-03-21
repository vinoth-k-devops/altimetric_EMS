using System;
using System.Security.Claims;
using EMS_Domain.Model;

namespace EMS_Service.Interfaces
{
	public interface ITokenService
	{
        Task<string> GenerateAccessToken(JWTSettings _jwtKEY, IEnumerable<Claim> claims);
        Task<string> GenerateRefreshToken();
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(JWTSettings _jwtKEY, string token);
    }
}

