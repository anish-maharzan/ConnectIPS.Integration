using System;

namespace ConnectIPS.Integration.Models.SAP
{
    class IncomingPayment
    {
        public string CardCode { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DueDate { get; set; }
        public double PaymentAmount { get; set; }
        public string DocCurrency { get; set; }
        public Invoice invoice { get; set; }
    }

    class Invoice
    {
        public int DocEntry { get; set; }
    }
}
