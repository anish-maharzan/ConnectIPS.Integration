using NepalPay.Library.Credentials;
using NepalPay.Library.Helpers;
using NepalPay.Library.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Implementation
{
    public static class AuthService
    {
        private static readonly HttpHelper httpHelper;
        static AuthService()
        {
            httpHelper = new HttpHelper("http://demo.connectips.com");
        }

        public static async Task<string> GetAccessTokenAsync()
        {
            var refreshToken = await GetRefreshTokenAsync();
            var accessToken = await GetAccessTokenAsync(refreshToken.refresh_token);
            return accessToken.access_token;
        }

        public static async Task<TokenResponse> GetAccessTokenAsync(string refreshToken)
        {
            string postUrl = ":9095/oauth/token";
            var objFormData = new
            {
                grant_type = "refresh_token",
                refresh_token = refreshToken
            };

            var formData = objFormData.GetType()
                .GetProperties()
                .ToDictionary(property => property.Name, property => property.GetValue(objFormData).ToString());
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{TransactionCredential.TBasicAuth.Username}:{TransactionCredential.TBasicAuth.Password}"));
            httpHelper.AddBasicAuthHeader(credentials);
            var response = await httpHelper.PostAsync<TokenResponse>(postUrl, formData);
            return response;
        }

        public static async Task<TokenResponse> GetRefreshTokenAsync()
        {
            string postUrl = ":6065/oauth/token";

            var formData = new Dictionary<string, string>();

            PropertyInfo[] properties = TransactionCredential.TUserAuth.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(TransactionCredential.TUserAuth).ToString();
                formData.Add(propertyName, propertyValue);
            }
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{TransactionCredential.TBasicAuth.Username}:{TransactionCredential.TBasicAuth.Password}"));
            httpHelper.AddBasicAuthHeader(credentials);
            var response = await httpHelper.PostAsync<TokenResponse>(postUrl, formData);
            return response;
        }
    }
}
