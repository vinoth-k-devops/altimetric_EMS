using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EMS_Common.Variables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EMS_Web_App.StaticFunc
{
	public static class StaticValue
	{
        public static bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }
        public static string UserType(IHttpContextAccessor _http, string ClaimType)
        {
            var token = _http.HttpContext?.Session.GetString(Constant.__TOKEN__);
            if(token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                {
                    var jwtSecurityToken = handler.ReadJwtToken(token);
                    return jwtSecurityToken.Claims.First(claim => claim.Type == ClaimType).Value;
                }
            }

            return string.Empty;
        }
        public static bool CheckUserActive(IHttpContextAccessor _http)
        {
            var token = _http.HttpContext?.Session.GetString(Constant.__TOKEN__);

            if (token != null)
                return IsValid(token);
            else
                return false;
        }
        public static string GetToken(IHttpContextAccessor _http)
        {
            var token = _http.HttpContext?.Session.GetString(Constant.__TOKEN__);

            if (token != null)
                return token;
            else
                return string.Empty;
        }
        
    }
    public static class GenericFunc
    {
        public static TResult ExtractJsonData<TResult>(string Json)
        {
            return JsonConvert.DeserializeObject<TResult>(Json)!;
        }
    }
}

