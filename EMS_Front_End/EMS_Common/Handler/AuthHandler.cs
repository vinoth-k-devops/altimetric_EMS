using System;
using Newtonsoft.Json;
using System.Text;

namespace EMS_Common.Handler
{
	public interface IAuthHandler
	{
		Task<Response> Login(string BaseURL, string MethodName, object model);
        Task<string> LogOut(string token, string BaseURL, string MethodName);

    }
	public class AuthHandler: IAuthHandler
	{
        public async Task<Response> Login(string BaseURL, string MethodName, object model)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(BaseURL);
                HttpResponseMessage response = await client.PostAsync(MethodName, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Response>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<string> LogOut(string token, string BaseURL, string MethodName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.PostAsync(MethodName, null);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
    }
    public class Response
    {
        public string UName { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}

