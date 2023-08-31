﻿using NepalPay.Library.Credentials;
using NepalPay.Library.Helpers;
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
    public class QRService : IDynamicQRService
    {
        private readonly HttpHelper httpHelper;

        public QRService()
        {
            httpHelper = new HttpHelper(QRCredential.BaseUrl);
        }

        public async Task<QRGenerationResponse> GenerateQRAsync(QRGeneration request)
        {
            var qrToken = await GetQRTokenAsync();
            var accessToken = qrToken.access_token;
            request.token = GetNepalPayToken(request);

            string url = "qr/generateQR";
            string requestBody = JsonConvert.SerializeObject(request);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.PostAsync<QRGenerationResponse>(url, requestBody);
            return response;
        }

        public async Task<IResponse> VerifyPayment(PaymentVerification request)
        {
            var qrToken = await GetQRTokenAsync();
            var accessToken = qrToken.access_token;

            string url = "nQR/v1/merchanttxnreport";
            string requestBody = JsonConvert.SerializeObject(request);
            httpHelper.AddBearerToken(accessToken);
            var responseString = await httpHelper.PostAsync(url, requestBody);
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
            string postUrl = "oauth/token";

            var formData = new Dictionary<string, string>();

            PropertyInfo[] properties = QRCredential.UserAuth.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(QRCredential.UserAuth).ToString();
                formData.Add(propertyName, propertyValue);
            }

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{QRCredential.BasicAuth.Username}:{QRCredential.BasicAuth.Password}"));
            httpHelper.AddBasicAuthHeader(credentials);
            var response = await httpHelper.PostAsync<TokenResponse>(postUrl, formData);
            return response;
        }

        private string GetNepalPayToken(QRGeneration request)
        {
            string tokenString = GetTokenString(request);
            var token = TokenService.GenerateNCHLToken(tokenString, QRCredential.PFXPassword, QRCredential.PFXPassword);
            return token;
        }

        private string GetTokenString(QRGeneration request)
        {
            string token = request.acquirerId + "," + request.merchantId + "," + request.merchantCategoryCode + "," + request.transactionCurrency + "," + request.transactionAmount + "," + request.billNumber + "," + QRCredential.UserAuth.username;
            return token;
        }
    }
}
