using NepalPay.Library.Models.Authentication;

namespace NepalPay.Library.Credentials
{
    public static class DynamicQRCredential
    {
        public static BasicAuthentication QrBasicAuth;
        public static UserAuthentication QrUserAuth;
        public static string MerchantCode;
        public static int MerchantCategoryCode;
        public static string MerchantName;
        public static string AcquirerId;
        public static string FileName;
        public static string BaseUrl;
        public static string Environment { get; set; }
        public static string PFXPassword { get; set; }
    }
}
