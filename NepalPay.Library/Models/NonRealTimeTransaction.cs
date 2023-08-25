using System.Collections.Generic;

namespace NepalPay.Library.Models
{
    public class NchlIpsBatchDetail
    {
        public string batchId { get; set; }
        public double batchAmount { get; set; }
        public int batchCount { get; set; }
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
        public NchlIpsBatchDetail()
        {
            batchCount = 1;
            batchCrncy = "NPR";
        }
    }

    public class NchlIpsTransactionDetail
    {
        public string instructionId { get; set; }
        public string endToEndId { get; set; }
        public double amount { get; set; }
        public string creditorAgent { get; set; }
        public string creditorBranch { get; set; }
        public string creditorName { get; set; }
        public string creditorAccount { get; set; }
        public string creditorIdValue { get; set; }
        public string creditorAddress { get; set; }
        public string creditorPhone { get; set; }
        public string creditorMobile { get; set; }
        public string creditorEmail { get; set; }
        public int addenda1 { get; set; }
        public string addenda2 { get; set; }
        public string addenda3 { get; set; }
        public string addenda4 { get; set; }
    }

    public class NonRealTimeTransaction
    {
        public NchlIpsBatchDetail nchlIpsBatchDetail { get; set; }
        public List<NchlIpsTransactionDetail> nchlIpsTransactionDetailList { get; set; }
        public string token { get; set; }
        public NonRealTimeTransaction()
        {
            nchlIpsTransactionDetailList = new List<NchlIpsTransactionDetail>();
        }
    }
}
