using System.Collections.Generic;

namespace ConnectIPS.Integration.Models.ConnectIps.Response
{
    public class PaymentVerificationResponse
    {
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public string billsPaymentDescription { get; set; }
        public string billsPaymentResponseCode { get; set; }
        public List<object> fieldErrors { get; set; }
        public string responseMessage { get; set; }
    }
}
