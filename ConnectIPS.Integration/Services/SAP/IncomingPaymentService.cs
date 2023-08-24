using ConnectIPS.Integration.Models.SAP;
using MainLibrary.SAPB1;
using SAPbobsCOM;

namespace ConnectIPS.Integration.Services.SAP
{
    class IncomingPaymentService
    {
        public void Add(IncomingPayment payment)
        {
            var incomingPayment = (Payments)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
            incomingPayment.CardCode = payment.CardCode; 
            incomingPayment.DocDate = payment.DocDate;
            incomingPayment.DueDate = payment.DueDate;
            incomingPayment.DocType = BoRcptTypes.rCustomer;
            incomingPayment.BPLID = payment.Branch;
            incomingPayment.ProjectCode = payment.Project;
            //incomingPayment.CashSum = payment.PaymentAmount; 
            //incomingPayment.DocCurrency = payment.DocCurrency;

            incomingPayment.Invoices.DocEntry =payment.invoice.DocEntry;
            incomingPayment.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
            incomingPayment.Invoices.SumApplied = payment.PaymentAmount;

            incomingPayment.TransferAccount = "CA12321028";
            incomingPayment.TransferSum = payment.PaymentAmount;

            if (incomingPayment.Add() != 0)
            {
                string error = $"Failed to add incoming payment: {B1Helper.DiCompany.GetLastErrorDescription()}";
                Program.SBO_Application.StatusBar.SetText(error, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            else
            {
                Program.SBO_Application.StatusBar.SetText("Incoming payment created successfully.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
        }
    }
}
