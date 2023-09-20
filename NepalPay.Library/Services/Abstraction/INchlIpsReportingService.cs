using NepalPay.Library.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Abstraction
{
    public interface INchlIpsReportingService
    {
        Task<List<NchlIpsReportByDateResponse>> GetTransactionReport(DateTime txnFromDate, DateTime txntoDate);
        Task<NchlIpsReportByBatchResponse> GetTransactionReport(string batchid);
    }
}
