using MainLibrary.SAPB1;
using NepalPay.Library.Models.Account;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using NepalPay.Library.Services.Implementation;
using SAPbobsCOM;

namespace ConnectIPS.Integration.Services
{
    public class NchlService
    {
        public static ValidateBankAccountResponse ValidateAccount(ValidateBankAccount request)
        {
            var bankAccountService = new BankAccountService();
            var response = bankAccountService.ValidateAccount(request).GetAwaiter().GetResult();
            return response;
        }

        public static NchlNpiResponse RealTimeTransfer(CIpsTransaction request)
        {
            var realTimeService = new CIpsService(request);
            NchlNpiResponse response = realTimeService.SendTransactionAsync().GetAwaiter().GetResult();
            return response;
        }

        public static NchlNpiResponse NonRealTimeTransfer(NchlIpsTransaction request)
        {
            var nonRealtimeService = new NchlIpsService(request);
            NchlNpiResponse response = nonRealtimeService.SendTransactionAsync().GetAwaiter().GetResult();
            return response;
        }

        public static bool ValidateTransferAmount(string transactionType, double amount, bool isSameBank, out string errMessage)
        {
            var isValid = transactionType == "RT" ? new CIpsService().ValidateTransferAmount(amount, isSameBank, out errMessage) :
                                                    new NchlIpsService().ValidateTransferAmount(amount, isSameBank, out errMessage);
            return isValid;
        }
    }
}
