﻿using NepalPay.Library.Models.Response;
using System.Threading.Tasks;

namespace NepalPay.Library.Services.Abstraction
{
    public interface INPIService
    {
        Task<CipsBatchResponseModel> SendTransactionAsync();
        bool ValidateTransferAmount(double transactionAmt, bool isSameBank, out string message);
        int GetChargeAmount(double transferAmount, bool isSameBank);
    }
}
