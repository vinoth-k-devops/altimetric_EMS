using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace EMS_Common.Handler
{
    public interface IAPIHandler<TModel> where TModel : class
    {
        Task<List<TModel>> GetAllData(string token, string BaseURL, string MethodName);
        Task<TModel> GetDataById(string token, string BaseURL, string MethodName, int? id);
        Task<string> AddData(string token, string BaseURL, string MethodName, TModel model);
        Task<string> UpdateData(string token, string BaseURL, string MethodName, TModel model);
        Task<string> AddDataWithOutToken(string BaseURL, string MethodName, TModel model);
    }
    public class APIHandler<TModel> : IAPIHandler<TModel> where TModel : class
    {
        public async Task<List<TModel>> GetAllData(string token, string BaseURL, string MethodName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.GetAsync(MethodName);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<TModel>>();
                }            
                else
                {
                    throw new NotImplementedException(response.RequestMessage!.ToString());
                }
            }
        }
        public async Task<TModel> GetDataById(string token, string BaseURL, string MethodName, int? id = 0)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.GetAsync(MethodName + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<TModel>();
                }
                else
                {
                    throw new NotImplementedException(response.RequestMessage!.ToString());
                }
            }
        }
        public async Task<string> AddData(string token, string BaseURL, string MethodName, TModel model)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.PostAsync(MethodName, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new NotImplementedException(response.RequestMessage!.ToString());
                }
            }                                     
        }
        public async Task<string> UpdateData(string token, string BaseURL, string MethodName, TModel model)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.PutAsync(MethodName, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new NotImplementedException(response.RequestMessage!.ToString());
                }
            }
        }
        public async Task<string> AddDataWithOutToken(string BaseURL, string MethodName, TModel model)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseURL);

                HttpResponseMessage response = await client.PostAsync(MethodName, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new NotImplementedException(response.RequestMessage!.ToString());
                }
            }
        }
    }
}

