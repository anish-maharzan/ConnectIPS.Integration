using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NepalPay.Library.Helpers
{
    public class HttpHelper
    {
        private readonly HttpClient _httpClient;

        public HttpHelper()
        {
            _httpClient = new HttpClient();
        }

        public void AddBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void AddBasicAuthHeader(string credentials)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        public async Task<string> GetAsync(string url)
        {
            
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> PostAsync<T>(string url, string requestBody)
        {
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMsg = await _httpClient.PostAsync(url, content);
                responseMsg.EnsureSuccessStatusCode();
                string response = await responseMsg.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<T>(response);
                return result;
        }

        public async Task<string> Post(string url, string requestBody)
        {
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMsg = await _httpClient.PostAsync(url, content);
                responseMsg.EnsureSuccessStatusCode();
                string response = await responseMsg.Content.ReadAsStringAsync();
                return response;
        }

        public async Task<T> PostFormData<T>(string url, Dictionary<string, string> formData)
        {
                HttpContent content = new FormUrlEncodedContent(formData);
                HttpResponseMessage responseMsg = await _httpClient.PostAsync(url, content);
                responseMsg.EnsureSuccessStatusCode();
                string response = await responseMsg.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<T>(response);
                return result;
        }
    }
}
