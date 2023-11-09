using NepalPay.Library.Models.Authentication;

namespace NepalPay.Library.Credentials
{
    public class NPICredential
    {
        public static BasicAuthentication BasicAuth;
        public static UserAuthentication UserAuth;
        public static string FileName;
        public static string BaseUrl;
        public static string Environment { get; set; }
        public static string PFXPassword { get; set; }
        public static string BatchPrefix { get; set; }
    }
}
