﻿namespace NepalPay.Library.Models.Response
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public object customerdetails { get; set; }
    }

}