using System;
using EMS_Common.Handler;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace EMS_Web_App.StaticFunc
{
	public class TokenClient
	{
		public string APIEndPoint = string.Empty;
        public string APIMethod = string.Empty;
        public string tokens = string.Empty;

        public TokenClient(string _APIEndPoint, string _APIMethod, string _token)
		{
			APIEndPoint = _APIEndPoint;
			APIMethod = _APIMethod;
			tokens = _token;
		}

		public async Task<TokenResponse> RequestRefreshTokenAsync(string refreshToken)
        {
            using (var client = new HttpClient())
            {
                var token = new TokenRequest()
                {
                    RefreshToken = refreshToken,
                    Token = tokens
                };
                var content = new StringContent(JsonConvert.SerializeObject(token), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(APIEndPoint);
                HttpResponseMessage response = await client.PostAsync(APIMethod, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<TokenResponse>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }

    }
    public class TokenRequest
    {

        public string Token { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
    public class TokenResponse
    {
        public string UName { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}

