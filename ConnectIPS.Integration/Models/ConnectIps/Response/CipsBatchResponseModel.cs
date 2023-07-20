using System.Collections.Generic;

namespace ConnectIPS.Integration.Models.ConnectIps.Response
{
    class CipsBatchResponseModel
    {
        public CipsBatchResponse cipsBatchResponse { get; set; }
        public List<CipsTxnResponse> cipsTxnResponseList { get; set; }
        public CipsBatchResponseModel()
        {
            cipsTxnResponseList = new List<CipsTxnResponse>();
        }
    }

    public class CipsBatchResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string batchId { get; set; }
        public string debitStatus { get; set; }
        public int id { get; set; }
    }

    public class CipsTxnResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public int id { get; set; }
        public string instructionId { get; set; }
        public string creditStatus { get; set; }
    }
}
