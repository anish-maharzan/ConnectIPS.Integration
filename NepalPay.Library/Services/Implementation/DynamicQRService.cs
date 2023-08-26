using NepalPay.Library.Credentials;
using NepalPay.Library.Helpers;
using NepalPay.Library.Models.Abstraction;
using NepalPay.Library.Models.Authentication;
using NepalPay.Library.Models.QR;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using NepalPay.Library.Services.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Implementation
{
    public class DynamicQRService : IDynamicQRService
    {
        private readonly BasicAuthentication QrBasicAuth;
        private readonly UserAuthentication QrUserAuth;
        private readonly HttpHelper httpHelper;
        private readonly string Filename;

        public DynamicQRService()
        {
            QrBasicAuth = DynamicQRCredential.QrBasicAuth;
            QrUserAuth = DynamicQRCredential.QrUserAuth;
            Filename = DynamicQRCredential.FileName;
            httpHelper = new HttpHelper();
        }
        
        public async Task<QRGenerationResponse> GenerateQRAsync(QRGeneration request)
        {
            var qrToken = await GetQRTokenAsync();
            var accessToken = qrToken.access_token;
            request.token = GetNepalPayToken(request);

            string url = "https://devopennpi.connectips.com/qr/generateQR";
            string requestBody = JsonConvert.SerializeObject(request);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.PostAsync<QRGenerationResponse>(url, requestBody);
            return response;
        }

        public async Task<IResponse> VerifyPayment(PaymentVerification request)
        {
            var qrToken = await GetQRTokenAsync();
            var accessToken = qrToken.access_token;

            string url = "https://devopennpi.connectips.com/nQR/v1/merchanttxnreport";
            string requestBody = JsonConvert.SerializeObject(request);
            httpHelper.AddBearerToken(accessToken);
            var responseString = await httpHelper.Post(url, requestBody);
            var response = JsonConvert.DeserializeObject<PaymentVerificationErrorResponse>(responseString);
            if (response.responseCode == "200")
            {
                var result = JsonConvert.DeserializeObject<PaymentVerificationSuccessResponse>(responseString);
                return result;
            }
            else
            {
                var result = JsonConvert.DeserializeObject<PaymentVerificationErrorResponse>(responseString);
                return result;
            }
        }

        public async Task<TokenResponse> GetQRTokenAsync()
        {
            string postUrl = "https://devopennpi.connectips.com/oauth/token";

            var formData = new Dictionary<string, string>();

            PropertyInfo[] properties = QrUserAuth.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(QrUserAuth).ToString();
                formData.Add(propertyName, propertyValue);
            }

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{QrBasicAuth.Username}:{QrBasicAuth.Password}"));
            httpHelper.AddBasicAuthHeader(credentials);
            var response = await httpHelper.PostFormData<TokenResponse>(postUrl, formData);
            return response;
        }

        private string GetNepalPayToken(QRGeneration request)
        {
            string tokenString = GetTokenString(request);

            var token = TokenService.GenerateNCHLToken(tokenString);
            return token;
        }

        private string GetTokenString(QRGeneration request)
        {
            string token = request.acquirerId + "," + request.merchantId + "," + request.merchantCategoryCode + "," + request.transactionCurrency + "," + request.transactionAmount + "," + request.billNumber + "," + QrUserAuth.username;
            return token;
        }

    }
}
