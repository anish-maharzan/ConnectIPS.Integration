using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConnectIPS.Integration.Helpers
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

        public void AddBasicAuthHeader(string username, string password)
        {
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        public async Task<string> Get(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                var error = $"Error occurred while making GET request: {ex.Message}";
                throw new Exception(error);
            }
        }

        public async Task<T> Post<T>(string url, string requestBody)
        {
            try
            {
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMsg = await _httpClient.PostAsync(url, content);
                responseMsg.EnsureSuccessStatusCode();
                string response = await responseMsg.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<T>(response);
                return result;
            }
            catch (HttpRequestException ex)
            {
                var error = $"Error occurred while making POST request: {ex.Message}";
                throw new Exception(error);
            }
        }

        public async Task<T> PostFormData<T>(string url, Dictionary<string, string> formData)
        {
            try
            {
                HttpContent content = new FormUrlEncodedContent(formData);
                HttpResponseMessage responseMsg = await _httpClient.PostAsync(url, content);
                responseMsg.EnsureSuccessStatusCode();
                string response = await responseMsg.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<T>(response);
                return result;
            }
            catch (HttpRequestException ex)
            {
                var error = $"Error occurred while making POST request: {ex.Message}";
                throw new Exception(error);
            }
        }
    }
}
