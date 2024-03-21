using System;
using System.IdentityModel.Tokens.Jwt;
using EMS_Common.Variables;
using EMS_Web_App.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace EMS_Web_App.StaticFunc
{
	public static class CustomTokenValidator 
    {
        public static async Task onTokenValidated(TokenValidatedContext context, string BaseURL)
        {
            var userPrincipal = context.Principal;

            if (context.Principal!.Identity!.IsAuthenticated)
            {
                var tokens = context.Properties.GetTokens();
                var refreshToken = tokens.FirstOrDefault(t => t.Name == Constant.__REFRESH_TOKEN__);
                var accessToken = tokens.FirstOrDefault(t => t.Name == Constant.__TOKEN__);
                var jwtSecurityToken = new JwtSecurityToken(accessToken!.Value);

                if (!StaticValue.IsValid(accessToken.Value))
                {
                    var tokenEndpoint = BaseURL;
                    var tokenClient = new TokenClient(tokenEndpoint, Constant.AuthRefreshToken, accessToken.Value);
                    var tokenResponse = await tokenClient.RequestRefreshTokenAsync(refreshToken!.Value);

                    if (tokenResponse == null)
                    {
                        context.Fail("Unauthorized");
                    }
                    //set new token values
                    refreshToken.Value = tokenResponse!.RefreshToken;
                    accessToken!.Value = tokenResponse.Token;

                    context.Properties.StoreTokens(tokens);
                }
                context.Success();
            }
        }
    }
}

