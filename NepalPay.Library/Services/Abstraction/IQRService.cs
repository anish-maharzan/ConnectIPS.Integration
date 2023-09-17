using NepalPay.Library.Models.QR;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Abstraction
{
    public interface IQRService
    {
        Task<QRGenerationResponse> GenerateQRAsync(QRGeneration request);
        Task<IResponse> VerifyPayment(PaymentVerification request);
    }
}