using ConnectIPS.Integration.Models;
using MainLibrary.SAPB1;
using SAPbobsCOM;
using System;

namespace ConnectIPS.Integration.Services
{
    public class PaymentService
    {
        public void AddIncomingPayment(IncomingPayment payment, out bool result, out string errMessage)
        {
            Payments incomingPayment = (Payments)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);

            incomingPayment.DocType = BoRcptTypes.rAccount;
            incomingPayment.BPLID = 3;


            //incomingPayment.CardCode = payment.CardCode;
            incomingPayment.DocDate = DateTime.Now;
            incomingPayment.DueDate = DateTime.Now;
            //incomingPayment.CashSum = payment.PaymentAmount;
            incomingPayment.DocCurrency = payment.DocCurrency;

            incomingPayment.AccountPayments.AccountCode = "C31110001";
            incomingPayment.AccountPayments.SumPaid = 100;

            incomingPayment.TransferSum = 100;
            incomingPayment.TransferAccount = "C31110002";
            //incomingPayment.TransferDate = DateTime.ParseExact(model.TransferDate, "yyyy-MM-dd", null);



            //incomingPayment.Invoices.DocEntry = payment.invoice.DocEntry;
            //incomingPayment.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
            //incomingPayment.Invoices.SumApplied = payment.PaymentAmount;

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

        public static void AddIncomingPayment()
        {
            Payments incomingPayment = (Payments)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);

            incomingPayment.DocType = BoRcptTypes.rAccount;
            incomingPayment.BPLID = 3;


            //incomingPayment.CardCode = payment.CardCode;
            incomingPayment.DocDate = DateTime.Now;
            incomingPayment.DueDate = DateTime.Now;
            //incomingPayment.CashSum = payment.PaymentAmount;
            //incomingPayment.DocCurrency = payment.DocCurrency;

            incomingPayment.AccountPayments.AccountCode = "C31110001";
            incomingPayment.AccountPayments.SumPaid = 500;

            incomingPayment.TransferSum = 500;
            incomingPayment.TransferAccount = "C31110002";
            //incomingPayment.TransferDate = DateTime.ParseExact(model.TransferDate, "yyyy-MM-dd", null);



            //incomingPayment.Invoices.DocEntry = payment.invoice.DocEntry;
            //incomingPayment.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
            //incomingPayment.Invoices.SumApplied = payment.PaymentAmount;

            if (incomingPayment.Add() != 0)
            {
                var result = false;
                var errMessage = B1Helper.DiCompany.GetLastErrorDescription();
            }
            else
            {
                var docEntry = B1Helper.DiCompany.GetNewObjectKey();
                var result = true;
                var errMessage = "";
            }
        }
    }
}
