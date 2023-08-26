namespace NepalPay.Library.Models.Transaction
{
    public class PaymentVerification
    {
        public string validationTraceId { get; set; }
        public string merchantId { get; set; }
        public string acquirerId { get; set; }
    }
}
