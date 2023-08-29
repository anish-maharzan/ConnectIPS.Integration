using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Abstraction
{
    public interface INonRealTimeService
    {
        Task<CipsBatchResponseModel> FundTransferAsync(NonRealTimeTransaction request);
    }
}
