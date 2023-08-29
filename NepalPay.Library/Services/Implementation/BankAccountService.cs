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
            httpHelper = new HttpHelper("http://demo.connectips.com");
        }
   
        public async Task<ValidateBankAccountResponse> ValidateAccount(ValidateBankAccount bankAccount)
        {
            string accessToken = await AuthService.GetAccessTokenAsync();

            string url = "http://demo.connectips.com:6065/api/validatebankaccount";
            string requestBody = JsonConvert.SerializeObject(bankAccount);
            httpHelper.AddBearerToken(accessToken);
            var response = await httpHelper.PostAsync<ValidateBankAccountResponse>(url, requestBody);
            return response;
        }
    }
}
