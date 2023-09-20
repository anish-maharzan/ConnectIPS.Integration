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
    public class NhclIpsReportingService : INchlIpsReportingService
    {
        private readonly HttpHelper httpHelper;

        public NhclIpsReportingService()
        {
            httpHelper = new HttpHelper(NPICredential.BaseUrl);

        }
        public async Task<List<NchlIpsReportByDateResponse>> GetTransactionReport(DateTime txnFromDate, DateTime txntoDate)
        {
            var refreshToken = await AuthService.GetRefreshTokenAsync();
            var accessToken = await AuthService.GetAccessTokenAsync(refreshToken.refresh_token);

            string url = "api/getnchlipstxnlistbydate";
            var data = new
            {
                txnDateFrom = txnFromDate.ToString("yyyy-MM-dd"),
                txnDateTo = txntoDate.ToString("yyyy-MM-dd")
            };
            string requestBody = JsonConvert.SerializeObject(data);
            httpHelper.AddBearerToken(accessToken.access_token);
            var response = await httpHelper.PostAsync<List<NchlIpsReportByDateResponse>>(url, requestBody);
            return response;
        }

        public async Task<NchlIpsReportByBatchResponse> GetTransactionReport(string batchid)
        {
            var refreshToken = await AuthService.GetRefreshTokenAsync();
            var accessToken = await AuthService.GetAccessTokenAsync(refreshToken.refresh_token);

            string url = "api/getnchlipstxnlistbybatchid";
            var data = new
            {
                batchId = batchid
            };
            string requestBody = JsonConvert.SerializeObject(data);
            httpHelper.AddBearerToken(accessToken.access_token);
            var response = await httpHelper.PostAsync<NchlIpsReportByBatchResponse>(url, requestBody);
            return response;
        }
    }
}
