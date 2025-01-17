
using ConnectIPS.Integration.Models.ConnectIps;
using ConnectIPS.Integration.Models.ConnectIps.Account;
using ConnectIPS.Integration.Models.ConnectIps.Response;
using ConnectIPS.Integration.Services.ConnectIps;
using MainLibrary.SAPB1;
using Newtonsoft.Json;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ConnectIPS.Integration.Forms.Outgoing
{

    [FormAttribute("426", "Forms/Outgoing/Outgoing Payments.b1f")]
    class Outgoing_Payments : SystemFormBase
    {
        public Outgoing_Payments()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.bAdd = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.txtCC = ((SAPbouiCOM.EditText)(this.GetItem("5").Specific));
            this.txtDN = ((SAPbouiCOM.EditText)(this.GetItem("3").Specific));
            this.txtDUE = ((SAPbouiCOM.EditText)(this.GetItem("12").Specific));
            this.txtRemarks = ((SAPbouiCOM.EditText)(this.GetItem("26").Specific));
            this.bAdd.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.BtnAdd_ClickBefore);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.cbCAT = ((SAPbouiCOM.ComboBox)(this.GetItem("cbCat").Specific));
            this.bPay = ((SAPbouiCOM.Button)(this.GetItem("bPay").Specific));
            this.bPay.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.bPay_ClickBefore);
            this.bPay.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bPay_ClickAfter);
            this.cVerified = ((SAPbouiCOM.CheckBox)(this.GetItem("cVerified").Specific));
            this.chBank = ((SAPbouiCOM.CheckBox)(this.GetItem("chBank").Specific));
            this.chBank.PressedAfter += new SAPbouiCOM._ICheckBoxEvents_PressedAfterEventHandler(this.chBank_PressedAfter);
            this.chBank.ClickAfter += new SAPbouiCOM._ICheckBoxEvents_ClickAfterEventHandler(this.chBank_ClickAfter);
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.cbType = ((SAPbouiCOM.ComboBox)(this.GetItem("cbType").Specific));
            this.bProcess = ((SAPbouiCOM.Button)(this.GetItem("bProc").Specific));
            this.bProcess.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bProcess_ClickAfter);
            this.chPay = ((SAPbouiCOM.CheckBox)(this.GetItem("chPay").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.tBat = ((SAPbouiCOM.EditText)(this.GetItem("tBat").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {

        }

        private SAPbouiCOM.Button bAdd;
        private SAPbouiCOM.EditText txtCC;
        private SAPbouiCOM.EditText txtDN;
        private SAPbouiCOM.EditText txtDUE;
        private SAPbouiCOM.EditText txtRemarks;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.ComboBox cbCAT;
        private SAPbouiCOM.CheckBox cVerified;
        private SAPbouiCOM.Button bPay;
        private double amount;
        private BankAccountDetail sender;
        private BankAccountDetail creditor;
        private SAPbouiCOM.CheckBox chBank;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.ComboBox cbType;
        private SAPbouiCOM.Button bProcess;
        private SAPbouiCOM.CheckBox chPay;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText tBat;

        private void OnCustomInitialize()
        {
            var bankCode = ConfigurationManager.AppSettings["BankCode"];
            sender = GetSenderDetails(bankCode);
        }

        private BankAccountDetail GetSenderDetails(string bankCode)
        {
            BankAccountDetail sender = null;
            string query = $@"SELECT T1.""AcctName"", T1.""Account"",  T0.""BankName"", T1.""UsrNumber1"", T1.""Branch"" FROM ODSC T0  INNER JOIN DSC1 T1 ON T0.""BankCode"" = T1.""BankCode"" WHERE T0.""BankCode"" ='{bankCode}'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            if (rec.RecordCount > 0)
            {
                sender = new BankAccountDetail()
                {
                    AccountName = rec.Fields.Item(0).Value.ToString(),
                    AccountNo = rec.Fields.Item(1).Value.ToString(),
                    BankName = rec.Fields.Item(2).Value.ToString(),
                    BankCode = rec.Fields.Item(3).Value.ToString(),
                    BranchCode = rec.Fields.Item(4).Value.ToString()
                };
            }
            return sender;
        }

        private BankAccountDetail GetReceiverDetails(string cardCode)
        {
            string query = $@"SELECT T1.""AcctName"", T1.""Account"", T2.""BankName"", T1.""MandateID"", T1.""Branch"" FROM OCRD T0  INNER JOIN OCRB T1 ON T0.""CardCode"" = T1.""CardCode"" INNER JOIN ODSC T2 ON T0.""BankCode"" = T2.""BankCode"" INNER JOIN DSC1 T3 ON T0.""BankCode"" = T3.""BankCode"" WHERE T0.""CardCode"" = '{cardCode}'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            if (rec.RecordCount > 0)
            {
                creditor = new BankAccountDetail()
                {
                    AccountName = rec.Fields.Item(0).Value.ToString(),
                    AccountNo = rec.Fields.Item(1).Value.ToString(),
                    BankName = rec.Fields.Item(2).Value.ToString(),
                    BankCode = rec.Fields.Item(3).Value.ToString(),
                    BranchCode = rec.Fields.Item(4).Value.ToString()
                };
            }
            return creditor;
        }

        private async Task<ValidateBankAccountResponse> ValidateAccountAsync(BankAccountDetail receiver)
        {
            var bankAccount = new ValidateBankAccount()
            {
                bankId = receiver.BankCode,
                accountId = receiver.AccountNo,
                accountName = receiver.AccountName
            };
            var connectIpsService = new ConnectIpsService();
            var response = await connectIpsService.ValidateAccount(bankAccount);
            return response;
        }

        private string GetBatch()
        {
            var randonNum = (new Random()).Next(100, 1000);
            var batchId = $"HIM-{txtDN.Value}-{randonNum}";
            tBat.Value = batchId;
            return batchId;
        }

        private string GetInstructionId()
        {
            var InstructionId = $"{GetBatch()}-1";
            return InstructionId;
        }

        private void BtnAdd_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE && chBank.Checked)
            {
                if (chBank.Checked && !chPay.Checked)
                {
                    bPay.Item.Click();
                    return;
                }

                if (!chPay.Checked)
                {
                    Program.SBO_Application.StatusBar.SetText("Please first process payment.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    BubbleEvent = false;
                    return;
                }
            }
        }

        private async Task<bool> PaymentValidationAsync()
        {
            string transactionType = cbType.Value;
            string categoryPurpose = cbCAT.Value;

            var valid = ValidateFundTransfer(transactionType, amount, sender.BankName, creditor.BankName, out string errMessage);
            if (!valid)
            {
                Program.SBO_Application.StatusBar.SetText(errMessage, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }

            var result = await ValidateAccountAsync(creditor);
            if (result.responseCode != "000")
            {
                if (result.matchPercentate < 80)
                {
                    Program.SBO_Application.MessageBox($"{result.responseMessage}");
                    return false;
                }
                else
                {
                    StringBuilder message = new StringBuilder();
                    message.AppendLine("Warning");
                    message.AppendLine($"{result.responseMessage}");
                    message.AppendLine("Do you want to continue?");
                    var input = Program.SBO_Application.MessageBox(message.ToString(), 1, "Yes", "No");

                    if (input == 2)
                        return false;
                }
            }
            else
            {
                cVerified.Checked = true;
            }
            return true;
        }

        private void bPay_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Initialize();

            string transactionType = cbType.Value;
            string categoryPurpose = cbCAT.Value;

            var valid = PaymentValidationAsync().GetAwaiter().GetResult();
            if (!valid)
                return;

            var charge = GetChargeAmt(transactionType, categoryPurpose, amount, sender.BankName, creditor.BankName);
            var tranConfirmModel = new TransactionConfirmationModel()
            {
                SenderBank = sender.BankName,
                SenderAcctNumber = sender.AccountNo,
                SenderAcctName = sender.AccountName,

                TransactionDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"),
                ReferenceID = $"ACCOUNTFT:{sender.AccountName}",
                TransactionDetail = txtRemarks.Value,
                CreditorBank = creditor.BankName,
                CreditorAcctName = creditor.AccountName,
                CreditorAcctNum = creditor.AccountNo,
                TransactionAmt = amount.ToString(),
                ChargeAmt = charge.ToString(),
                TotalAmt = (amount + charge).ToString()
            };

            var confirmationForm = new TransactionConfimation(UIAPIRawForm.Type, UIAPIRawForm.TypeCount, tranConfirmModel);
            confirmationForm.Show();
        }

        private void Initialize()
        {
            var vendor = txtCC.Value;
            creditor = GetReceiverDetails(vendor);

            var amountString = txtDUE.Value.Split(' ')[1];
            double.TryParse(amountString, out amount);
        }

        public void ConfirmTransaction()
        {
            RealTimeTransferAsync(sender, creditor).GetAwaiter().GetResult();
        }

        public void CancelTransaction()
        {
        }

        private bool ValidateFundTransfer(string transactionType, double transactionAmt, string senderBank, string receiverBank, out string message)
        {
            if (transactionType == "RT")
            {
                if (senderBank == receiverBank)
                {
                    if (transactionAmt > 10000000)
                    {
                        message = "Real Time Transaction Amount exceed";
                        return false;
                    }
                }
                else
                {
                    if (transactionAmt > 2000000)
                    {
                        message = "Real Time Transaction Amount exceed";
                        return false;
                    }
                }
            }
            else
            {
                if (senderBank == receiverBank)
                {
                    message = "Transaction is not permitted in same bank for Non-Real Time";
                    return false;
                }
                else
                {
                    if (transactionAmt > 200000000)
                    {
                        message = "Non-Real Time Transaction Amount exceed";
                        return false;
                    }
                }
            }
            message = "";
            return true;
        }

        private int GetChargeAmt(string transactionType, string categoryPurpose, double amount, string senderBank, string receiverBank)
        {
            int chargeAmt = 0;
            int upperLimit = 0;
            if (senderBank == receiverBank)
                upperLimit = 10000000;
            else
                upperLimit = 2000000;

            if (transactionType == "RT")
            {
                if (amount >= 0.01 && amount < 500)
                    chargeAmt = 2;
                else if (amount >= 500 && amount < 5000)
                    chargeAmt = 5;
                else if (amount >= 5000 && amount < 50000)
                    chargeAmt = 10;
                else if (amount >= 50000 && amount < upperLimit)
                    chargeAmt = 15;
            }
            else
            {
                if (amount >= 0.01 && amount < 500)
                    chargeAmt = 2;
                else if (amount >= 500 && amount < 50000)
                    chargeAmt = 5;
                else if (amount >= 50000 && amount < 200000000)
                    chargeAmt = 10;
            }
            return chargeAmt;
        }

        private void chBank_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

        }

        private void chBank_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (chBank.Checked)
                CheckBank(true);
            else
                CheckBank(false);
        }

        private void CheckBank(bool isChecked)
        {
            try
            {
                txtCC.Item.Click();
            }
            catch (Exception)
            {
            }

            bPay.Item.Enabled = isChecked;
            cbType.Item.Enabled = isChecked;
            cbCAT.Item.Enabled = isChecked;

            if (!isChecked)
            {
                cbType.Select("", SAPbouiCOM.BoSearchKey.psk_ByValue);
                cbCAT.Select("", SAPbouiCOM.BoSearchKey.psk_ByValue);
            }
        }

        private void bPay_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!PayButtonValidation())
                BubbleEvent = false;
        }

        private bool PayButtonValidation()
        {
            if (chPay.Checked)
            {
                Program.SBO_Application.StatusBar.SetText("Already paid.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(txtCC.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select vendor.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(txtDUE.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please enter amount.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (!chBank.Checked)
            {
                Program.SBO_Application.StatusBar.SetText("Please check NCHL-NPI (Bank Integration).", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(cbType.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select Transaction Type.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(cbCAT.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select Category Purpose.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(txtRemarks.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please enter remarks.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private void bProcess_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                string transactionType = cbType.Value;
                string categoryPurpose = cbCAT.Value;

                CipsBatchResponseModel response = new CipsBatchResponseModel();
                if (transactionType == "RT")
                    response = RealTimeTransferAsync(sender, creditor).GetAwaiter().GetResult();
                else
                    response = NonRealTimeTransferAsync(sender, creditor).GetAwaiter().GetResult();

                if (response.cipsBatchResponse.responseCode != "000")
                {
                    //Program.SBO_Application.MessageBox($"{response.cipsBatchResponse.responseMessage}");                    
                    Program.SBO_Application.MessageBox($"payment failed.");
                }
                else
                {
                    chPay.Checked = true;
                    Program.SBO_Application.MessageBox($"{response.cipsBatchResponse.responseMessage}");
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private async Task<CipsBatchResponseModel> RealTimeTransferAsync(BankAccountDetail sender, BankAccountDetail receiver)
        {
            var realTime = GetRealTimeData(sender, receiver);
            var connectIpsService = new ConnectIpsService();
            CipsBatchResponseModel response = await connectIpsService.RealTimeFundTransferAsync(realTime);
            return response;
        }

        private RealTimeTransaction GetRealTimeData(BankAccountDetail sender, BankAccountDetail receiver)
        {
            var cipsBatchDetail = new CIpsBatchDetail()
            {
                batchId = GetBatch(),
                batchAmount = amount.ToString(),
                debtorAgent = sender.BankCode,
                debtorBranch = sender.BranchCode,
                debtorName = sender.AccountName,
                debtorAccount = sender.AccountNo,
                categoryPurpose = cbCAT.Value
            };

            var cipsTransactionDetailList = new CIpsTransactionDetail()
            {
                instructionId = GetInstructionId(),
                endToEndId = txtRemarks.Value,
                amount = amount.ToString(),
                creditorAgent = receiver.BankCode,
                creditorBranch = receiver.BranchCode,
                creditorName = receiver.AccountName,
                creditorAccount = receiver.AccountNo,

            };

            var realTime = new RealTimeTransaction
            {
                cipsBatchDetail = cipsBatchDetail
            };
            realTime.cipsTransactionDetailList.Add(cipsTransactionDetailList);

            return realTime;
        }

        private async Task<CipsBatchResponseModel> NonRealTimeTransferAsync(BankAccountDetail sender, BankAccountDetail receiver)
        {
            var realTime = GetNonRealTimeData(sender, receiver);
            var connectIpsService = new ConnectIpsService();
            CipsBatchResponseModel response = await connectIpsService.NonRealTimeFundTransferAsync(realTime);
            return response;
        }

        private NonRealTimeTransaction GetNonRealTimeData(BankAccountDetail sender, BankAccountDetail receiver)
        {
            var cipsBatchDetail = new NchlIpsBatchDetail()
            {
                batchId = GetBatch(),
                batchAmount = amount,
                debtorAgent = sender.BankCode,
                debtorBranch = sender.BranchCode,
                debtorName = sender.AccountName,
                debtorAccount = sender.AccountNo,
                categoryPurpose = cbCAT.Value
            };

            var cipsTransactionDetailList = new NchlIpsTransactionDetail()
            {
                instructionId = GetInstructionId(),
                endToEndId = txtRemarks.Value,
                amount = amount,
                creditorAgent = receiver.BankCode,
                creditorBranch = receiver.BranchCode,
                creditorName = receiver.AccountName,
                creditorAccount = receiver.AccountNo,
            };

            var realTime = new NonRealTimeTransaction
            {
                nchlIpsBatchDetail = cipsBatchDetail
            };
            realTime.nchlIpsTransactionDetailList.Add(cipsTransactionDetailList);

            return realTime;
        }
    }
}
