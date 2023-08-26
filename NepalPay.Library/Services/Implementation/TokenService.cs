using NepalPay.Library.Credentials;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NepalPay.Library.Services.Implementation
{
    public static class TokenService
    {
        public static string GenerateNCHLToken(string stringToHash, string pfxPassword = "123")
        {
            try
            {
                using (var crypt = new SHA256Managed())
                using (var cert = new X509Certificate2(DynamicQRCredential.FileName, pfxPassword, X509KeyStorageFlags.Exportable))
                {
                    byte[] data = Encoding.UTF8.GetBytes(stringToHash);

                    RSA csp = null;
                    if (cert != null)
                    {
                        csp = cert.PrivateKey as RSA;
                    }

                    if (csp == null)
                    {
                        throw new Exception("No valid cert was found");
                    }

                    csp.ImportParameters(csp.ExportParameters(true));
                    byte[] signatureByte = csp.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                    string tokenStringForReference = Convert.ToBase64String(signatureByte);
                    return Convert.ToBase64String(signatureByte);
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetTokenString(string BatchString, string TransactionString, string userId)
        {
            var tokenString = $"{BatchString},{TransactionString},{userId}";
            return tokenString;
        }

        public static string GetTransactionString(string InstructionId, string CreditorAgent, string CreditorBranch, string CreditorAccount, string TransactionAmount)
        {
            var transactionString = $"{InstructionId},{CreditorAgent},{CreditorBranch},{CreditorAccount},{TransactionAmount}";
            return transactionString;
        }
    }
}
