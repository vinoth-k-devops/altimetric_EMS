using System;
using Newtonsoft.Json;
using System.Text;
using EMS_Common.Variables;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http;

namespace EMS_Common.Handler
{
    public interface ITransactionHandler
    {
        Task<List<object>> GetActiveList(string BaseURL, string MethodName);
        Task<List<object>> GetActiveList(string token, string BaseURL, string MethodName);
        Task<List<object>> GetActiveCity(string BaseURL, string MethodName, int id);
        Task<Dashboard> GetDashboard(string token, string BaseURL, string MethodName);
        Task<string> UpdateUserToActive(string token, string BaseURL, string MethodName, string strId);
        Task<List<election_year_to_date>> GetElectionList(string token, string BaseURL, string MethodName);

        Task<APIResponse> AddData(string token, string BaseURL, string MethodName, object model);
        Task<APIResponse> UpdateData(string token, string BaseURL, string MethodName, object model);
        Task<APIResponse> GetDataById(string token, string BaseURL, string MethodName, string Id);
        Task<APIResponse> GetDataList(string token, string BaseURL, string MethodName);
        Task<APIResponse> GetUserDashboard(string token, string BaseURL, string MethodName);
    }
    public class TransactionHandler : ITransactionHandler
	{
        public async Task<List<object>> GetActiveList(string BaseURL, string MethodName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                HttpResponseMessage response = await client.GetAsync(MethodName);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<object>>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<List<object>> GetActiveList(string token, string BaseURL, string MethodName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage response = await client.GetAsync(MethodName);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<object>>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<List<object>> GetActiveCity(string BaseURL, string MethodName, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                HttpResponseMessage response = await client.GetAsync(MethodName + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<object>>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<Dashboard> GetDashboard(string token, string BaseURL, string MethodName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.GetAsync(MethodName);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Dashboard>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<string> UpdateUserToActive(string token, string BaseURL, string MethodName, string strId)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(strId), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.PostAsync(MethodName, content);

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
        public async Task<List<election_year_to_date>> GetElectionList(string token, string BaseURL, string MethodName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage response = await client.GetAsync(MethodName);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<election_year_to_date>>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<APIResponse> AddData(string token, string BaseURL, string MethodName, object model)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.PostAsync(MethodName, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<APIResponse>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<APIResponse> UpdateData(string token, string BaseURL, string MethodName, object model)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage response = await client.PutAsync(MethodName, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<APIResponse>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<APIResponse> GetDataById(string token, string BaseURL, string MethodName, string Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage response = await client.GetAsync(MethodName + Id);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<APIResponse>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<APIResponse> GetDataList(string token, string BaseURL, string MethodName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage response = await client.GetAsync(MethodName);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<APIResponse>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
        public async Task<APIResponse> GetUserDashboard(string token, string BaseURL, string MethodName)
        {
            using (var client = new HttpClient())
            {
                //var content = new StringContent(JsonConvert.SerializeObject(token), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage response = await client.GetAsync(MethodName + token);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<APIResponse>();
                }
                else
                {
                    throw new NotImplementedException("No records found.");
                }
            }
        }
    }
}

