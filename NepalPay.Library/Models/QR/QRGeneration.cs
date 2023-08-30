using NepalPay.Library.Credentials;
using System;

namespace NepalPay.Library.Models.QR
{
    public class QRGeneration
    {
        public QRGeneration()
        {
            pointOfInitialization = 12;
            acquirerId = QRCredential.AcquirerId;
            merchantId = QRCredential.MerchantCode;
            merchantName = QRCredential.MerchantName;
            merchantCategoryCode = Convert.ToInt32(QRCredential.MerchantCategoryCode);
            merchantCountry = "NP";
            merchantCity = "Kathmandu";
            merchantPostalCode = "4600";
            merchantLanguage = "en";
            transactionCurrency = 524;

            referenceLabel = null;
            mobileNo = null;
            valueOfConvenienceFeeFixed = "0.00";

            storeLabel = "Store1";
            terminalLabel = "Terminal1";
            purposeOfTransaction = "Bill payment";
            additionalConsumerDataRequest = null;
            loyaltyNumber = null;
        }

        public int pointOfInitialization { get; set; }
        public string acquirerId { get; set; }
        public string merchantId { get; set; }
        public string merchantName { get; set; }
        public int merchantCategoryCode { get; set; }
        public string merchantCountry { get; set; }
        public string merchantCity { get; set; }
        public string merchantPostalCode { get; set; }
        public string merchantLanguage { get; set; }
        public int transactionCurrency { get; set; }
        public string transactionAmount { get; set; }
        public string valueOfConvenienceFeeFixed { get; set; }
        public string billNumber { get; set; }
        public string referenceLabel { get; set; }
        public string mobileNo { get; set; }
        public string storeLabel { get; set; }
        public string terminalLabel { get; set; }
        public string purposeOfTransaction { get; set; }
        public string additionalConsumerDataRequest { get; set; }
        public string loyaltyNumber { get; set; }
        public string token { get; set; }
    }
}
