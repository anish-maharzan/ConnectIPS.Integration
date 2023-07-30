using ConnectIPS.Integration.Models.ConnectIps.Interface;
using System;
using System.Collections.Generic;

namespace ConnectIPS.Integration.Models.ConnectIps.Response
{
    public class PaymentVerificationErrorResponse: IResponse
    {
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public string billsPaymentDescription { get; set; }
        public string billsPaymentResponseCode { get; set; }
        public List<object> fieldErrors { get; set; }
        public string responseMessage { get; set; }
    }

    public class ResponseBody
    {
        public string SessionSrlNo { get; set; }
        public DateTime recDate { get; set; }
        public string instructionId { get; set; }
        public string nQrTxnId { get; set; }
        public string acquirerId { get; set; }
        public string issuerId { get; set; }
        public string network { get; set; }
        public string issuerNetwork { get; set; }
        public double amount { get; set; }
        public int interchangeFee { get; set; }
        public double transactionFee { get; set; }
        public string debitStatus { get; set; }
        public string creditStatus { get; set; }
        public string payerName { get; set; }
        public string tranType { get; set; }
        public string payerMobileNumber { get; set; }
        public string merchantName { get; set; }
        public string merchantTxnRef { get; set; }
        public object terminal { get; set; }
        public string merchantBillNo { get; set; }
        public object instrument { get; set; }
        public object validationTraceId { get; set; }
        public object merchantPan { get; set; }
        public object nfcTxnId { get; set; }
    }

    public class PaymentVerificationSuccessResponse : IResponse
    {
        public DateTime timestamp { get; set; }
        public string responseCode { get; set; }
        public string responseStatus { get; set; }
        public string responseMessage { get; set; }
        public List<ResponseBody> responseBody { get; set; }
    }
}
