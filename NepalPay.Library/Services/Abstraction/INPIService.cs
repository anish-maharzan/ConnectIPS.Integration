using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Abstraction
{
    public interface INPIService
    {
        Task<CipsBatchResponseModel> FundTransferAsync();
        bool ValidateTransferAmount(double transactionAmt, bool isSameBank, out string message);
        int GetChargeAmount(double transferAmount, bool isSameBank);
    }
}
