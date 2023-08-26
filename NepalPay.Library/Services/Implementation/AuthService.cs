using NepalPay.Library.Credentials;
using NepalPay.Library.Helpers;
using NepalPay.Library.Models.Response;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Implementation
{
    public static class AuthService 
    {
        private static readonly HttpHelper httpHelper;
        static AuthService()
        {
            httpHelper = new HttpHelper();
        }

        public static async Task<string> GetAccessTokenAsync()
        {
            var refreshToken = await GetRefreshTokenAsync();
            var accessToken = await GetAccessTokenAsync(refreshToken.refresh_token);
            return accessToken.access_token;
        }

        public static async Task<TokenResponse> GetAccessTokenAsync(string refreshToken)
        {
            string postUrl = "http://demo.connectips.com:9095/oauth/token";

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

            httpHelper.AddBasicAuthHeader(TransactionCredential.TBasicAuth.Username, TransactionCredential.TBasicAuth.Password);
            var response = await httpHelper.PostFormData<TokenResponse>(postUrl, formData);
            return response;
        }

        public static async Task<TokenResponse> GetRefreshTokenAsync()
        {
            string postUrl = "http://demo.connectips.com:6065/oauth/token";

            var formData = new Dictionary<string, string>();

            PropertyInfo[] properties = TransactionCredential.TUserAuth.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(TransactionCredential.TUserAuth).ToString();
                formData.Add(propertyName, propertyValue);
            }

            httpHelper.AddBasicAuthHeader(TransactionCredential.TBasicAuth.Username, TransactionCredential.TBasicAuth.Password);
            var response = await httpHelper.PostFormData<TokenResponse>(postUrl, formData);
            return response;
        }
    }
}
