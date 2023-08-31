using NepalPay.Library.Credentials;
using NepalPay.Library.Helpers;
using NepalPay.Library.Models.Response;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Implementation
{
    public static class AuthService
    {
        public static async Task<TokenResponse> GetRefreshTokenAsync()
        {
            HttpHelper httpHelper;
            if (NPICredential.Environment == "Test")
                httpHelper = new HttpHelper("http://demo.connectips.com:6065/");
            else
                httpHelper = new HttpHelper(NPICredential.BaseUrl);

            string postUrl = "oauth/token";

            var formData = new Dictionary<string, string>();

            PropertyInfo[] properties = NPICredential.UserAuth.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(NPICredential.UserAuth).ToString();
                formData.Add(propertyName, propertyValue);
            }
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{NPICredential.BasicAuth.Username}:{NPICredential.BasicAuth.Password}"));
            httpHelper.AddBasicAuthHeader(credentials);
            var response = await httpHelper.PostAsync<TokenResponse>(postUrl, formData);
            return response;
        }

        public static async Task<TokenResponse> GetAccessTokenAsync(string refreshToken)
        {
            HttpHelper httpHelper;
            if (NPICredential.Environment == "Test")
                httpHelper = new HttpHelper("http://demo.connectips.com:9095/");
            else
                httpHelper = new HttpHelper(NPICredential.BaseUrl);

            string postUrl = "oauth/token";

            var formData = new Dictionary<string, string>();
            var objFormData = new
            {
                grant_type = "refresh_token",
                refresh_token = refreshToken
            };

            PropertyInfo[] properties = objFormData.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(objFormData).ToString();
                formData.Add(propertyName, propertyValue);
            }

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{NPICredential.BasicAuth.Username}:{NPICredential.BasicAuth.Password}"));
            httpHelper.AddBasicAuthHeader(credentials);
            var response = await httpHelper.PostAsync<TokenResponse>(postUrl, formData);
            return response;
        }
    }
}