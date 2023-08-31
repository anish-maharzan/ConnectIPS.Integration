using NepalPay.Library.Credentials;
using NepalPay.Library.Helpers;
using NepalPay.Library.Models.Account;
using NepalPay.Library.Services.Abstraction;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Implementation
{
    public class BankAccountService : IBankAccountService
    {
        private readonly HttpHelper httpHelper;

        public BankAccountService()
        {
            httpHelper = new HttpHelper(NPICredential.BaseUrl);
        }
   
        public async Task<ValidateBankAccountResponse> ValidateAccount(ValidateBankAccount bankAccount)
        {
            var refreshToken = await AuthService.GetRefreshTokenAsync();
            var accessToken = await AuthService.GetAccessTokenAsync(refreshToken.refresh_token);

            string url = "api/validatebankaccount";
            string requestBody = JsonConvert.SerializeObject(bankAccount);
            httpHelper.AddBearerToken(accessToken.access_token);
            var response = await httpHelper.PostAsync<ValidateBankAccountResponse>(url, requestBody);
            return response;
        }
    }
}
