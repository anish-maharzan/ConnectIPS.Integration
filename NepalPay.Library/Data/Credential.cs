using NepalPay.Library.Models;

namespace NepalPay.Library.Data
{
    public static class Credential
    {
        static Credential()
        {
            TBasicAuth = new BasicAuthentication()
            {
                Username = "*****",
                Password = "*****"
            };

            TUserAuth = new UserAuthentication()
            {
                username = "*****",
                password = "*****",
                grant_type = "*****"
            };

            QRUserAuth = new UserAuthentication()
            {
                username = "*****",
                password = "*****",
                grant_type = "*****"
            };

            QRBasicAuth = new BasicAuthentication()
            {
                Username = "*****",
                Password = "*****"
            };

            MerchantCode = "*****";
            MerchantCategoryCode = 0;
            MerchantName = "*****";
            AcquirerId = "*****";
        }
        public static UserAuthentication TUserAuth { get; set; }
        public static BasicAuthentication TBasicAuth { get; set; }

        public static UserAuthentication QRUserAuth { get; set; }
        public static BasicAuthentication QRBasicAuth { get; set; }

        public static string MerchantCode { get; set; }
        public static int MerchantCategoryCode { get; set; }
        public static string MerchantName { get; set; }
        public static string AcquirerId { get; set; }
    }
}
