using System.Collections.Generic;

namespace NepalPay.Library.Models.Transaction
{
    public class CIpsBatchDetail
    {
        public string batchId { get; set; }
        public string batchAmount { get; set; }
        public string batchCount { get; set; }
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

        public CIpsBatchDetail()
        {
            batchCount = "1";
            batchCrncy = "NPR";
            //debtorIdType = "";
            //debtorIdValue = "";
            //debtorAddress = "";
            //debtorPhone = "";
            //debtorMobile = "";
            //debtorEmail = "";
        }
    }

    public class CIpsTransactionDetail
    {
        public string instructionId { get; set; }
        public string endToEndId { get; set; }
        public string amount { get; set; }
        public string creditorAgent { get; set; }
        public string creditorBranch { get; set; }
        public string creditorName { get; set; }
        public string creditorAccount { get; set; }
        public string creditorIdType { get; set; }
        public string creditorIdValue { get; set; }
        public string creditorAddress { get; set; }
        public string creditorPhone { get; set; }
        public string creditorMobile { get; set; }
        public string creditorEmail { get; set; }
        public string addenda1 { get; set; }
        public string addenda2 { get; set; }
        public string addenda3 { get; set; }
        public string addenda4 { get; set; }

        public CIpsTransactionDetail()
        {
            //creditorIdType = "";
            //creditorIdValue = "";
            //creditorAddress = "";
            //creditorPhone = "";
            //creditorMobile = "";
            //creditorEmail = "";
            //addenda1 = "";
            //addenda2 = "";
            //addenda3 = "";
            //addenda4 = "";
        }
    }

    public class RealTimeTransaction
    {
        public CIpsBatchDetail cipsBatchDetail { get; set; }
        public List<CIpsTransactionDetail> cipsTransactionDetailList { get; set; }
        public string token { get; set; }
        public RealTimeTransaction()
        {
            cipsTransactionDetailList = new List<CIpsTransactionDetail>();
        }
    }

}
