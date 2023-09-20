using System;
using System.Collections.Generic;

namespace NepalPay.Library.Models.Response
{
    public class NchlIpsBatchDetail
    {
        public int id { get; set; }
        public string batchId { get; set; }
        public string recDate { get; set; }
        public string channelId { get; set; }
        public string ipsBatchId { get; set; }
        public string fileName { get; set; }
        public double batchAmount { get; set; }
        public int batchCount { get; set; }
        public double batchChargeAmount { get; set; }
        public string batchCrncy { get; set; }
        public string categoryPurpose { get; set; }
        public string debtorAgent { get; set; }
        public string debtorBranch { get; set; }
        public string debtorName { get; set; }
        public string debtorAccount { get; set; }
        public object debtorIdType { get; set; }
        public object debtorIdValue { get; set; }
        public object debtorAddress { get; set; }
        public object debtorPhone { get; set; }
        public object debtorMobile { get; set; }
        public object debtorEmail { get; set; }
        public object sessionSeq { get; set; }
        public object settlementDate { get; set; }
        public string rcreUserId { get; set; }
        public string rcreTime { get; set; }
        public string debitStatus { get; set; }
        public object debitReasonCode { get; set; }
        public int isoTxnId { get; set; }
        public int corporateId { get; set; }
        public object initBranchId { get; set; }
        public string debitReasonDesc { get; set; }
        public string txnResponse { get; set; }
        public object stpFlag { get; set; }
    }

    public class NchlIpsTransactionDetail
    {
        public int id { get; set; }
        public string recDate { get; set; }
        public string instructionId { get; set; }
        public string endToEndId { get; set; }
        public double amount { get; set; }
        public double chargeAmount { get; set; }
        public string chargeLiability { get; set; }
        public object purpose { get; set; }
        public string creditorAgent { get; set; }
        public string creditorBranch { get; set; }
        public string creditorName { get; set; }
        public string creditorAccount { get; set; }
        public object creditorIdType { get; set; }
        public object creditorIdValue { get; set; }
        public object creditorAddress { get; set; }
        public object creditorPhone { get; set; }
        public object creditorMobile { get; set; }
        public object creditorEmail { get; set; }
        public object addenda1 { get; set; }
        public string addenda2 { get; set; }
        public object addenda3 { get; set; }
        public object addenda4 { get; set; }
        public object freeCode1 { get; set; }
        public object freeCode2 { get; set; }
        public object freeText1 { get; set; }
        public object freeText2 { get; set; }
        public object freeText3 { get; set; }
        public object freeText4 { get; set; }
        public string rcreUserId { get; set; }
        public string rcreTime { get; set; }
        public string ipsBatchId { get; set; }
        public string creditStatus { get; set; }
        public object reasonCode { get; set; }
        public object refId { get; set; }
        public object remarks { get; set; }
        public string particulars { get; set; }
        public object merchantId { get; set; }
        public string appId { get; set; }
        public object appTxnId { get; set; }
        public object reversalStatus { get; set; }
        public object isoTxnId { get; set; }
        public int batchId { get; set; }
        public string ipsTxnId { get; set; }
        public string reasonDesc { get; set; }
        public string txnResponse { get; set; }
    }

    public class NchlIpsReportByDateResponse
    {
        public NchlIpsBatchDetail nchlIpsBatchDetail { get; set; }
        public List<NchlIpsTransactionDetail> nchlIpsTransactionDetailList { get; set; }
        public object token { get; set; }
    }
}