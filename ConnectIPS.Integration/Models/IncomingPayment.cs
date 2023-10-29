using System;

namespace ConnectIPS.Integration.Models
{
    public class IncomingPayment
    {
        public string CardCode { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DueDate { get; set; }
        public double PaymentAmount { get; set; }
        public string DocCurrency { get; set; }
        public Invoice invoice { get; set; }
    }

    public class Invoice
    {
        public int DocEntry { get; set; }
    }
}
