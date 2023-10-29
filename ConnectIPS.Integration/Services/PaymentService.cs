using ConnectIPS.Integration.Models;
using MainLibrary.SAPB1;
using SAPbobsCOM;

namespace ConnectIPS.Integration.Services
{
    public class PaymentService
    {
        public void AddIncomingPayment(IncomingPayment payment, out bool result, out string errMessage)
        {
            Payments incomingPayment = (Payments)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
            incomingPayment.CardCode = payment.CardCode;
            incomingPayment.DocDate = payment.DocDate;
            incomingPayment.DueDate = payment.DueDate;
            incomingPayment.DocType = BoRcptTypes.rCustomer;
            incomingPayment.CashSum = payment.PaymentAmount;
            incomingPayment.DocCurrency = payment.DocCurrency;



            incomingPayment.Invoices.DocEntry = payment.invoice.DocEntry;
            incomingPayment.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
            incomingPayment.Invoices.SumApplied = payment.PaymentAmount;

            if (incomingPayment.Add() != 0)
            {
                result = false;
                errMessage = B1Helper.DiCompany.GetLastErrorDescription();
            }
            else
            {
                result = true;
                errMessage = "";
            }
        }
    }
}
