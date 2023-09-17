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
    public class NchlIpsService : INchlNpiService
    {
        private readonly HttpHelper httpHelper;
        private readonly NchlIpsTransaction data;
        private static readonly double upperLimit = 2000000;

        public NchlIpsService()
        {
        }

        public NchlIpsService(NchlIpsTransaction request)
        {
            httpHelper = new HttpHelper(NPICredential.BaseUrl);
            data = request;
        }

        public async Task<CipsBatchResponseModel> SendTransactionAsync()
        {
            var refreshToken = await AuthService.GetRefreshTokenAsync();
            var accessToken = await AuthService.GetAccessTokenAsync(refreshToken.refresh_token);
            data.token = GetNepalPayToken(data);

            string url = "api/postnchlipsbatch";
            string requestBody = JsonConvert.SerializeObject(data);
            httpHelper.AddBearerToken(accessToken.access_token);
            var response = await httpHelper.PostAsync<CipsBatchResponseModel>(url, requestBody);
            return response;
        }

        public bool ValidateTransferAmount(double transactionAmt, bool isSameBank, out string message)
        {
            if (isSameBank)
            {
                message = "Transaction is not permitted in same bank for Non-Real Time";
                return false;
            }
            else if (transactionAmt > upperLimit)
            {
                message = "Non-Real Time Transaction Amount exceed";
                return false;
            }
            else
            {
                message = "";
                return true;
            }
        }

        public int GetChargeAmount(double transferAmount, bool isSameBank)
        {
            int chargeAmount = 0;
            if (transferAmount >= 0.01 && transferAmount < 500)
                chargeAmount = 2;
            else if (transferAmount >= 500 && transferAmount < 50000)
                chargeAmount = 5;
            else if (transferAmount >= 50000 && transferAmount < upperLimit)
                chargeAmount = 10;
            return chargeAmount;
        }

        private string GetNepalPayToken(NchlIpsTransaction request)
        {
            var batchDetail = request.nchlIpsBatchDetail;
            var batchString = GetBatchString(batchDetail.batchId, batchDetail.debtorAgent, batchDetail.debtorBranch, batchDetail.debtorAccount, batchDetail.batchAmount.ToString(), batchDetail.batchCrncy, batchDetail.categoryPurpose);

            var transDetail = request.nchlIpsTransactionDetailList.First();
            var transactionString = TokenService.GetTransactionString(transDetail.instructionId, transDetail.creditorAgent, transDetail.creditorBranch, transDetail.creditorAccount, transDetail.amount.ToString());

            var tokenString = TokenService.GetTokenString(batchString, transactionString, NPICredential.UserAuth.username);

            var token = TokenService.GenerateNCHLToken(NPICredential.FileName, tokenString, NPICredential.PFXPassword);
            return token;
        }

        private string GetBatchString(string BatchId, string DebtorAgent, string DebtorBranch, string DebtorAccount, string BatchAmount, string BatchCurrency, string categoryPurpose)
        {
            var batchString = $"{BatchId},{DebtorAgent},{DebtorBranch},{DebtorAccount},{BatchAmount},{BatchCurrency},{categoryPurpose}";
            return batchString;
        }

    }
}
