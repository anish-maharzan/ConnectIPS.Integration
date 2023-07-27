using ConnectIPS.Integration.Helpers;
using ConnectIPS.Integration.Models.ConnectIps;
using ConnectIPS.Integration.Models.ConnectIps.Account;
using ConnectIPS.Integration.Models.ConnectIps.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectIPS.Integration.Services.ConnectIps
{
    class ConnectIpsService
    {
        private readonly BasicAuthentication basicAuth;
        private readonly UserAuthentication userAuth;
        private readonly string baseUrl;
        private readonly string Filename;
        private readonly HttpHelper httpHelper;

        public ConnectIpsService()
        {
            basicAuth = new BasicAuthentication()
            {
                Username = "neosoft",
                Password = "Abcd@123"
            };

            userAuth = new UserAuthentication()
            {
                username = "NEOSOFT@999",
                password = "123Abcd@123",
                grant_type = "password"
            };

            baseUrl = "http://demo.connectips.com:6065";
            Filename = Application.StartupPath+ "\\Files\\NPI.pfx";
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

        private string GenerateConnectIPSToken(string stringToHash, string pfxPassword = "123")
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

        private string GetConnectIpsToken(RealTimeTransaction request)
        {
            var batchDetail = request.cipsBatchDetail;
            var batchString = GetBatchString(batchDetail.batchId, batchDetail.debtorAgent, batchDetail.debtorBranch, batchDetail.debtorAccount, batchDetail.batchAmount, batchDetail.batchCrncy);

            var transDetail = request.cipsTransactionDetailList.First();
            var transactionString = GetTransactionString(transDetail.instructionId, transDetail.creditorAgent, transDetail.creditorBranch, transDetail.creditorAccount, transDetail.amount);

            var tokenString = GetTokenString(batchString, transactionString, userAuth.username);

            var token = GenerateConnectIPSToken(tokenString);
            return token;
        }

        private string GetConnectIpsToken(NonRealTimeTransaction request)
        {
            var batchDetail = request.nchlIpsBatchDetail;
            var batchString = GetBatchString(batchDetail.batchId, batchDetail.debtorAgent, batchDetail.debtorBranch, batchDetail.debtorAccount, batchDetail.batchAmount.ToString(), batchDetail.batchCrncy, batchDetail.categoryPurpose);

            var transDetail = request.nchlIpsTransactionDetailList.First();
            var transactionString = GetTransactionString(transDetail.instructionId, transDetail.creditorAgent, transDetail.creditorBranch, transDetail.creditorAccount, transDetail.amount.ToString());

            var tokenString = GetTokenString(batchString, transactionString, userAuth.username);

            var token = GenerateConnectIPSToken(tokenString);
            return token;
        }

        private async Task<TokenResponse> GetRefreshTokenAsync()
        {
            string postUrl = "http://demo.connectips.com:6065/oauth/token";

            var formData = new Dictionary<string, string>();

            PropertyInfo[] properties = userAuth.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(userAuth).ToString();
                formData.Add(propertyName, propertyValue);
            }

            httpHelper.AddBasicAuthHeader(basicAuth.Username, basicAuth.Password);
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

            httpHelper.AddBasicAuthHeader(basicAuth.Username, basicAuth.Password);
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
            request.token = GetConnectIpsToken(request);

            string url = "http://demo.connectips.com:6065/api/postcipsbatch";
            string requestBody = JsonConvert.SerializeObject(request);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.Post<CipsBatchResponseModel>(url, requestBody);
            return response;
        }

        public async Task<CipsBatchResponseModel> NonRealTimeFundTransferAsync(NonRealTimeTransaction request)
        {
            string accessToken = await GetAccessTokenAsync();
            request.token = GetConnectIpsToken(request);

            string url = "http://demo.connectips.com:6065/api/postnchlipsbatch";
            string requestBody = JsonConvert.SerializeObject(request);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.Post<CipsBatchResponseModel>(url, requestBody);
            return response;
        }

    }
}
