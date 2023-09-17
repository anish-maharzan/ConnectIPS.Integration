using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Response.Report;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Abstraction
{
    public interface IReportingService
    {
        Task<List<CIpsReportByDateResponse>> GetTransactionReport(DateTime txnFromDate, DateTime txntoDate);
        Task<CIpsReportByBatchResponse> GetTransactionReport(string batchid);
    }
}
