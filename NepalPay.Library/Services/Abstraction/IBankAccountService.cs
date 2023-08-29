using NepalPay.Library.Models.Account;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Abstraction
{
    public interface IBankAccountService
    {
        Task<ValidateBankAccountResponse> ValidateAccount(ValidateBankAccount bankAccount);
    }
}
