using NepalPay.Library.Credentials;
using NepalPay.Library.Helpers;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Response.Report;
using NepalPay.Library.Services.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Implementation
{
    public class CIpsReportingService : IReportingService
    {
        private readonly HttpHelper httpHelper;

        public CIpsReportingService()
        {
            httpHelper = new HttpHelper(NPICredential.BaseUrl);

        }
        public async Task<List<CIpsReportByDateResponse>> GetTransactionReport(DateTime txnFromDate, DateTime txntoDate)
        {
            var refreshToken = await AuthService.GetRefreshTokenAsync();
            var accessToken = await AuthService.GetAccessTokenAsync(refreshToken.refresh_token);

            string url = "api/getcipstxnlistbydate";
            var data = new
            {
                txnDateFrom = txnFromDate.ToString("yyyy-MM-dd"),
                txnDateTo = txntoDate.ToString("yyyy-MM-dd")
            };
            string requestBody = JsonConvert.SerializeObject(data);
            httpHelper.AddBearerToken(accessToken.access_token);
            var response = await httpHelper.PostAsync<List<CIpsReportByDateResponse>>(url, requestBody);
            return response;
        }

        public async Task<CIpsReportByBatchResponse> GetTransactionReport(string batchid)
        {
            var refreshToken = await AuthService.GetRefreshTokenAsync();
            var accessToken = await AuthService.GetAccessTokenAsync(refreshToken.refresh_token);

            string url = "api/getcipstxnlistbybatchid";
            var data = new
            {
                batchId = batchid
            };
            string requestBody = JsonConvert.SerializeObject(data);
            httpHelper.AddBearerToken(accessToken.access_token);
            var response = await httpHelper.PostAsync<CIpsReportByBatchResponse>(url, requestBody);
            return response;
        }
    }
}
