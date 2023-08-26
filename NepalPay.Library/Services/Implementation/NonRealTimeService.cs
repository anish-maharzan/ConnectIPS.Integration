using NepalPay.Library.Credentials;
using NepalPay.Library.Helpers;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using NepalPay.Library.Services.Abstraction;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

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
            var response = await httpHelper.PostAsync<CipsBatchResponseModel>(url, requestBody);
            return response;
        }

        private string GetNepalPayToken(NonRealTimeTransaction request)
        {
            var batchDetail = request.nchlIpsBatchDetail;
            var batchString = GetBatchString(batchDetail.batchId, batchDetail.debtorAgent, batchDetail.debtorBranch, batchDetail.debtorAccount, batchDetail.batchAmount.ToString(), batchDetail.batchCrncy, batchDetail.categoryPurpose);

            var transDetail = request.nchlIpsTransactionDetailList.First();
            var transactionString = TokenService.GetTransactionString(transDetail.instructionId, transDetail.creditorAgent, transDetail.creditorBranch, transDetail.creditorAccount, transDetail.amount.ToString());

            var tokenString = TokenService.GetTokenString(batchString, transactionString, TransactionCredential.TUserAuth.username);

            var token = TokenService.GenerateNCHLToken(tokenString);
            return token;
        }

        private string GetBatchString(string BatchId, string DebtorAgent, string DebtorBranch, string DebtorAccount, string BatchAmount, string BatchCurrency, string categoryPurpose)
        {
            var batchString = $"{BatchId},{DebtorAgent},{DebtorBranch},{DebtorAccount},{BatchAmount},{BatchCurrency},{categoryPurpose}";
            return batchString;
        }

    }
}
