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
    public class RealTimeService : INPIService
    {
        private readonly HttpHelper httpHelper;
        private readonly RealTimeTransaction _request;
        private static readonly double upperLimitSameBank = 2000000; 
        private static readonly double upperLimitDiffBank = 10000000;

        public RealTimeService()
        {
            httpHelper = new HttpHelper(NPICredential.BaseUrl);
        }

        public RealTimeService(RealTimeTransaction request)
        {
            httpHelper = new HttpHelper(NPICredential.BaseUrl);
            _request = request;
        }

        public async Task<CipsBatchResponseModel> SendTransactionAsync()
        {
            var refreshToken = await AuthService.GetRefreshTokenAsync();
            var accessToken = await AuthService.GetAccessTokenAsync(refreshToken.refresh_token);
            _request.token = GetNepalPayToken(_request);

            string url = "api/postcipsbatch";
            string requestBody = JsonConvert.SerializeObject(_request);
            httpHelper.AddBearerToken(accessToken.access_token);
            var response = await httpHelper.PostAsync<CipsBatchResponseModel>(url, requestBody);
            return response;
        }

        public bool ValidateTransferAmount(double transferAmount, bool isSameBank, out string message)
        {
            if (isSameBank && transferAmount > upperLimitSameBank)
            {
                message = "Real Time Transaction Amount exceed for same bank";
                return false;
            }
            else if (transferAmount > upperLimitDiffBank)
            {
                message = "Real Time Transaction Amount exceed for different bank";
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
            double upperLimit = isSameBank ? upperLimitSameBank : upperLimitDiffBank;
            int chargeAmount = 0;

            if (transferAmount >= 0.01 && transferAmount < 500)
                chargeAmount = 2;
            else if (transferAmount >= 500 && transferAmount < 5000)
                chargeAmount = 5;
            else if (transferAmount >= 5000 && transferAmount < 50000)
                chargeAmount = 10;
            else if (transferAmount >= 50000 && transferAmount < upperLimit)
                chargeAmount = 15;

            return chargeAmount;
        }

        private string GetNepalPayToken(RealTimeTransaction request)
        {
            var batchDetail = request.cipsBatchDetail;
            var batchString = GetBatchString(batchDetail.batchId, batchDetail.debtorAgent, batchDetail.debtorBranch, batchDetail.debtorAccount, batchDetail.batchAmount, batchDetail.batchCrncy);

            var transDetail = request.cipsTransactionDetailList.First();
            var transactionString = TokenService.GetTransactionString(transDetail.instructionId, transDetail.creditorAgent, transDetail.creditorBranch, transDetail.creditorAccount, transDetail.amount);

            var tokenString = TokenService.GetTokenString(batchString, transactionString, NPICredential.UserAuth.username);

            var token = TokenService.GenerateNCHLToken(NPICredential.FileName, tokenString, NPICredential.PFXPassword);
            return token;
        }

        private string GetBatchString(string BatchId, string DebtorAgent, string DebtorBranch, string DebtorAccount, string BatchAmount, string BatchCurrency)
        {
            var batchString = $"{BatchId},{DebtorAgent},{DebtorBranch},{DebtorAccount},{BatchAmount},{BatchCurrency}";
            return batchString;
        }
    }
}
