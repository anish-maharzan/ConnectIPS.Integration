namespace ConnectIPS.Integration.Models.ConnectIps.Account
{
    class ValidateBankAccountResponse
    {
        public string bankId { get; set; }
        public string branchId { get; set; }
        public string accountId { get; set; }
        public string accountName { get; set; }
        public string currency { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public int matchPercentate { get; set; }
        public string baseUrl { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
