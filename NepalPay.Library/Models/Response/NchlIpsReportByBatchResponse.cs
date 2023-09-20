using System;
using System.Collections.Generic;

namespace NepalPay.Library.Models.Response
{
    public class NchlIpsReportByBatchResponse
    {
        public int id { get; set; }
        public string batchId { get; set; }
        public string recDate { get; set; }
        public int isoTxnId { get; set; }
        public double batchAmount { get; set; }
        public int batchCount { get; set; }
        public double batchChargeAmount { get; set; }
        public string batchCrncy { get; set; }
        public string categoryPurpose { get; set; }
        public string debtorAgent { get; set; }
        public string debtorBranch { get; set; }
        public string debtorName { get; set; }
        public string debtorAccount { get; set; }
        public string debtorIdType { get; set; }
        public string debtorIdValue { get; set; }
        public string debtorAddress { get; set; }
        public string debtorPhone { get; set; }
        public string debtorMobile { get; set; }
        public string debtorEmail { get; set; }
        public string channelId { get; set; }
        public string debitStatus { get; set; }
        public string debitReasonCode { get; set; }
        public string ipsBatchId { get; set; }
        public string fileName { get; set; }
        public string rcreTime { get; set; }
        public string rcreUserId { get; set; }
        public string sessionSeq { get; set; }
        public string settlementDate { get; set; }
        public string debitReasonDesc { get; set; }
        public string txnResponse { get; set; }
        public List<NchlIpsTransactionDetail> nchlIpsTransactionDetailList { get; set; }
    }
}
