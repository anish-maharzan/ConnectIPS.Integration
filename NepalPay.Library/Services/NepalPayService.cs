using NepalPay.Library.Data;
using NepalPay.Library.Helpers;
using NepalPay.Library.Models;
using NepalPay.Library.Models.Account;
using NepalPay.Library.Models.Interface;
using NepalPay.Library.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NepalPay.Library.Services
{
    public class NepalPayService
    {
        private readonly BasicAuthentication TBasicAuth;
        private readonly UserAuthentication TUserAuth;
        private readonly BasicAuthentication QrBasicAuth;
        private readonly UserAuthentication QrUserAuth;
        private readonly HttpHelper httpHelper;
        private readonly string Filename;

        public NepalPayService()
        {
            TBasicAuth = Credential.TBasicAuth;
            TUserAuth = Credential.TUserAuth;
            QrUserAuth = Credential.QRUserAuth;
            QrBasicAuth = Credential.QRBasicAuth;

            Filename= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\NPI.pfx";
            httpHelper = new HttpHelper();
        }

        private string GetBatchString(string BatchId, string DebtorAgent, string DebtorBranch, string DebtorAccount, string BatchAmount, string BatchCurrency)
        {
            var batchString = $"{BatchId},{DebtorAgent},{DebtorBranch},{DebtorAccount},{BatchAmount},{BatchCurrency}";
            return batchString;
        }

        private string GetBatchString(string BatchId, string DebtorAgent, string DebtorBranch, string DebtorAccount, string BatchAmount, string BatchCurrency, string categoryPurpose)
        {
            var batchString = $"{BatchId},{DebtorAgent},{DebtorBranch},{DebtorAccount},{BatchAmount},{BatchCurrency},{categoryPurpose}";
            return batchString;
        }

        private string GetTransactionString(string InstructionId, string CreditorAgent, string CreditorBranch, string CreditorAccount, string TransactionAmount)
        {
            var transactionString = $"{InstructionId},{CreditorAgent},{CreditorBranch},{CreditorAccount},{TransactionAmount}";
            return transactionString;
        }

        private string GetTokenString(string BatchString, string TransactionString, string userId)
        {
            var tokenString = $"{BatchString},{TransactionString},{userId}";
            return tokenString;
        }

        private string GetTokenString(QRGeneration request)
        {
            string token = request.acquirerId + "," + request.merchantId + "," + request.merchantCategoryCode + "," + request.transactionCurrency + "," + request.transactionAmount + "," + request.billNumber + "," + QrUserAuth.username;
            return token;
        }

        private string GenerateNepalPayToken(string stringToHash, string pfxPassword = "123")
        {
            try
            {
                using (var crypt = new SHA256Managed())
                using (var cert = new X509Certificate2(Filename, pfxPassword, X509KeyStorageFlags.Exportable))
                {
                    byte[] data = Encoding.UTF8.GetBytes(stringToHash);

                    RSA csp = null;
                    if (cert != null)
                    {
                        csp = cert.PrivateKey as RSA;
                    }

                    if (csp == null)
                    {
                        throw new Exception("No valid cert was found");
                    }

                    csp.ImportParameters(csp.ExportParameters(true));
                    byte[] signatureByte = csp.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                    string tokenStringForReference = Convert.ToBase64String(signatureByte);
                    return Convert.ToBase64String(signatureByte);
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private string GetNepalPayToken(RealTimeTransaction request)
        {
            var batchDetail = request.cipsBatchDetail;
            var batchString = GetBatchString(batchDetail.batchId, batchDetail.debtorAgent, batchDetail.debtorBranch, batchDetail.debtorAccount, batchDetail.batchAmount, batchDetail.batchCrncy);

            var transDetail = request.cipsTransactionDetailList.First();
            var transactionString = GetTransactionString(transDetail.instructionId, transDetail.creditorAgent, transDetail.creditorBranch, transDetail.creditorAccount, transDetail.amount);

            var tokenString = GetTokenString(batchString, transactionString, TUserAuth.username);

            var token = GenerateNepalPayToken(tokenString);
            return token;
        }

        private string GetNepalPayToken(NonRealTimeTransaction request)
        {
            var batchDetail = request.nchlIpsBatchDetail;
            var batchString = GetBatchString(batchDetail.batchId, batchDetail.debtorAgent, batchDetail.debtorBranch, batchDetail.debtorAccount, batchDetail.batchAmount.ToString(), batchDetail.batchCrncy, batchDetail.categoryPurpose);

            var transDetail = request.nchlIpsTransactionDetailList.First();
            var transactionString = GetTransactionString(transDetail.instructionId, transDetail.creditorAgent, transDetail.creditorBranch, transDetail.creditorAccount, transDetail.amount.ToString());

            var tokenString = GetTokenString(batchString, transactionString, TUserAuth.username);

            var token = GenerateNepalPayToken(tokenString);
            return token;
        }

        private string GetNepalPayToken(QRGeneration request)
        {
            string tokenString = GetTokenString(request);

            var token = GenerateNepalPayToken(tokenString);
            return token;
        }

        private async Task<TokenResponse> GetRefreshTokenAsync()
        {
            string postUrl = "http://demo.connectips.com:6065/oauth/token";

            var formData = new Dictionary<string, string>();

            PropertyInfo[] properties = TUserAuth.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(TUserAuth).ToString();
                formData.Add(propertyName, propertyValue);
            }

            httpHelper.AddBasicAuthHeader(TBasicAuth.Username, TBasicAuth.Password);
            var response = await httpHelper.PostFormData<TokenResponse>(postUrl, formData);
            return response;
        }

        private async Task<TokenResponse> GetAccessTokenAsync(string refreshToken)
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

            httpHelper.AddBasicAuthHeader(TBasicAuth.Username, TBasicAuth.Password);
            var response = await httpHelper.PostFormData<TokenResponse>(postUrl, formData);
            return response;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var refreshToken = await GetRefreshTokenAsync();
            var accessToken = await GetAccessTokenAsync(refreshToken.refresh_token);
            return accessToken.access_token;
        }

        public async Task<ValidateBankAccountResponse> ValidateAccount(ValidateBankAccount bankAccount)
        {
            string accessToken = await GetAccessTokenAsync();

            string url = "http://demo.connectips.com:6065/api/validatebankaccount";
            string requestBody = JsonConvert.SerializeObject(bankAccount);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.Post<ValidateBankAccountResponse>(url, requestBody);
            return response;
        }

        public async Task<CipsBatchResponseModel> RealTimeFundTransferAsync(RealTimeTransaction request)
        {
            string accessToken = await GetAccessTokenAsync();
            request.token = GetNepalPayToken(request);

            string url = "http://demo.connectips.com:6065/api/postcipsbatch";
            string requestBody = JsonConvert.SerializeObject(request);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.Post<CipsBatchResponseModel>(url, requestBody);
            return response;
        }

        public async Task<CipsBatchResponseModel> NonRealTimeFundTransferAsync(NonRealTimeTransaction request)
        {
            string accessToken = await GetAccessTokenAsync();
            request.token = GetNepalPayToken(request);

            string url = "http://demo.connectips.com:6065/api/postnchlipsbatch";
            string requestBody = JsonConvert.SerializeObject(request);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.Post<CipsBatchResponseModel>(url, requestBody);
            return response;
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

            httpHelper.AddBasicAuthHeader(QrBasicAuth.Username, QrBasicAuth.Password);
            var response = await httpHelper.PostFormData<TokenResponse>(postUrl, formData);
            return response;
        }

        public async Task<QRGenerationResponse> GenerateQRAsync(QRGeneration request)
        {
            var qrToken = await GetQRTokenAsync();
            var accessToken = qrToken.access_token;
            request.token = GetNepalPayToken(request);

            string url = "https://devopennpi.connectips.com/qr/generateQR";
            string requestBody = JsonConvert.SerializeObject(request);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.Post<QRGenerationResponse>(url, requestBody);
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

        public async Task GetReportAsync()
        {
            var qrToken = await GetQRTokenAsync();
            var accessToken = qrToken.access_token;
        }
    }
}
