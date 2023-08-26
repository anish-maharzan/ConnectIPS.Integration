using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NepalPay.Library.Helpers;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using NepalPay.Library.Services.Abstraction;
using Newtonsoft.Json;
using NepalPay.Library.Credentials;

namespace NepalPay.Library.Services.Implementation
{
    public class NonRealTimeService : INonRealTimeService
    {
        private readonly HttpHelper httpHelper;
        private readonly NonRealTimeTransaction _request;

        public NonRealTimeService(NonRealTimeTransaction request)
        {
            httpHelper = new HttpHelper();
            _request = request;

        }
        
        public async Task<CipsBatchResponseModel> FundTransferAsync(NonRealTimeTransaction request)
        {
            string accessToken = await AuthService.GetAccessTokenAsync();
            _request.token = GetNepalPayToken(_request);

            string url = "http://demo.connectips.com:6065/api/postnchlipsbatch";
            string requestBody = JsonConvert.SerializeObject(_request);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.Post<CipsBatchResponseModel>(url, requestBody);
            return response;
        }

        private string GetNepalPayToken(NonRealTimeTransaction request)
        {
            var batchDetail = request.nchlIpsBatchDetail;
            var batchString = GetBatchString(batchDetail.batchId, batchDetail.debtorAgent, batchDetail.debtorBranch, batchDetail.debtorAccount, batchDetail.batchAmount.ToString(), batchDetail.batchCrncy, batchDetail.categoryPurpose);

            var transDetail = request.nchlIpsTransactionDetailList.First();
            var transactionString = GetTransactionString(transDetail.instructionId, transDetail.creditorAgent, transDetail.creditorBranch, transDetail.creditorAccount, transDetail.amount.ToString());

            var tokenString = GetTokenString(batchString, transactionString, TransactionCredential.TUserAuth.username);

            var token = GenerateNepalPayToken(tokenString);
            return token;
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

        private string GenerateNepalPayToken(string stringToHash, string pfxPassword = "123")
        {
            try
            {
                using (var crypt = new SHA256Managed())
                using (var cert = new X509Certificate2(NCHLCredential.FileName, pfxPassword, X509KeyStorageFlags.Exportable))
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

    }
}
