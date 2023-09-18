using ConnectIPS.Integration.Forms.Users;
using MainLibrary.SAPB1;
using NepalPay.Library.Models.Account;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using NepalPay.Library.Services.Implementation;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConnectIPS.Integration
{

    [FormAttribute("426", "Forms/SAP/Outgoing Payments.b1f")]
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
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.cbCAT = ((SAPbouiCOM.ComboBox)(this.GetItem("cbCat").Specific));
            this.bPay = ((SAPbouiCOM.Button)(this.GetItem("bPay").Specific));
            this.bPay.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.bPay_ClickBefore);
            this.bPay.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bPay_ClickAfter);
            this.cVerified = ((SAPbouiCOM.CheckBox)(this.GetItem("cVerified").Specific));
            this.chBank = ((SAPbouiCOM.CheckBox)(this.GetItem("chBank").Specific));
            this.chBank.PressedAfter += new SAPbouiCOM._ICheckBoxEvents_PressedAfterEventHandler(this.chBank_PressedAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.cbType = ((SAPbouiCOM.ComboBox)(this.GetItem("cbType").Specific));
            this.bProcess = ((SAPbouiCOM.Button)(this.GetItem("bProc").Specific));
            this.bProcess.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bProcess_ClickAfter);
            this.chPay = ((SAPbouiCOM.CheckBox)(this.GetItem("chPay").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.tBat = ((SAPbouiCOM.EditText)(this.GetItem("tBat").Specific));
            this.bAdd = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.bAdd.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.bAdd_PressedAfter);
            this.bAdd.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.BtnAdd_ClickBefore);
            this.txtCC = ((SAPbouiCOM.EditText)(this.GetItem("5").Specific));
            this.txtDN = ((SAPbouiCOM.EditText)(this.GetItem("3").Specific));
            this.txtDUE = ((SAPbouiCOM.EditText)(this.GetItem("12").Specific));
            this.txtRemarks = ((SAPbouiCOM.EditText)(this.GetItem("26").Specific));
            this.LblWTax = ((SAPbouiCOM.StaticText)(this.GetItem("151").Specific));
            this.TxtWTax = ((SAPbouiCOM.EditText)(this.GetItem("152").Specific));
            this.LblCurr = ((SAPbouiCOM.StaticText)(this.GetItem("23").Specific));
            this.TxtCurr = ((SAPbouiCOM.EditText)(this.GetItem("21").Specific));
            this.BtnCancel = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.OptionVendor = ((SAPbouiCOM.OptionBtn)(this.GetItem("57").Specific));
            this.OptionAccount = ((SAPbouiCOM.OptionBtn)(this.GetItem("58").Specific));
            this.TxtGLA = ((SAPbouiCOM.EditText)(this.GetItem("TxtGLA").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("71").Specific));
            this.Matrix0.ChooseFromListBefore += new SAPbouiCOM._IMatrixEvents_ChooseFromListBeforeEventHandler(this.TxtGLAccount_ChooseFromListBefore);
            this.TxtAcctAmount = ((SAPbouiCOM.EditText)(this.GetItem("10000167").Specific));
            this.TxID = ((SAPbouiCOM.EditText)(this.GetItem("TxID").Specific));
            this.TxCode = ((SAPbouiCOM.EditText)(this.GetItem("TxCode").Specific));
            this.TxMsg = ((SAPbouiCOM.EditText)(this.GetItem("TxMsg").Specific));
            this.OnCustomInitialize();
        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {

        }

        private void OnCustomInitialize()
        {
            FormSetting();
        }

        private SAPbouiCOM.Button bAdd;
        private SAPbouiCOM.EditText txtCC;
        private SAPbouiCOM.EditText txtDN;
        private SAPbouiCOM.EditText txtDUE;
        private SAPbouiCOM.EditText txtRemarks;
        private SAPbouiCOM.StaticText LblWTax;
        private SAPbouiCOM.EditText TxtWTax;
        private SAPbouiCOM.StaticText LblCurr;
        private SAPbouiCOM.EditText TxtCurr;
        private SAPbouiCOM.Button BtnCancel;
        private SAPbouiCOM.Matrix Matrix0;
        private SAPbouiCOM.OptionBtn OptionVendor;
        private SAPbouiCOM.OptionBtn OptionAccount;
        private SAPbouiCOM.EditText TxtGLA;
        private SAPbouiCOM.EditText TxtAcctAmount;

        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.ComboBox cbCAT;
        private SAPbouiCOM.CheckBox cVerified;
        private SAPbouiCOM.Button bPay;
        private SAPbouiCOM.CheckBox chBank;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.ComboBox cbType;
        private SAPbouiCOM.Button bProcess;
        private SAPbouiCOM.CheckBox chPay;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText tBat;
        private SAPbouiCOM.EditText TxID;
        private SAPbouiCOM.EditText TxCode;
        private SAPbouiCOM.EditText TxMsg;

        private double amount;
        private BankAccount sender;
        private BankAccount receiver;

        private void bPay_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!ValidatePayButton())
            {
                BubbleEvent = false;
                return;
            }
        }

        private void bPay_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                if (OptionVendor.Selected)
                {
                    SetSenderDetails();
                    SetVendorDetails();
                    var amountString = txtDUE.Value.Split(' ')[1];
                    double.TryParse(amountString, out amount);

                    if (!ValidateTransferAmount())
                        return;

                    var result = ValidateAccountAsync().GetAwaiter().GetResult();

                    if (result.responseCode == "000")
                    {
                        cVerified.Checked = true;
                    }
                    else
                    {
                        if (result.matchPercentate < 80)
                        {
                            Program.SBO_Application.MessageBox($"{result.responseMessage}");
                            return;
                        }
                        else
                        {
                            StringBuilder message = new StringBuilder();
                            message.AppendLine("Warning");
                            message.AppendLine($"{result.responseMessage}");
                            message.AppendLine("Do you want to continue?");
                            var input = Program.SBO_Application.MessageBox(message.ToString(), 1, "Yes", "No");

                            if (input == 2)
                                return;
                        }
                    }
                    DisplayTransactionConfirmation();
                }
                else if (OptionAccount.Selected)
                {
                    SetSenderDetails();
                    SetReceiverDetails();
                    var amountString = TxtAcctAmount.Value.Split(' ')[1];
                    double.TryParse(amountString, out amount);

                    if (!ValidateTransferAmount())
                        return;

                    var result = ValidateAccountAsync().GetAwaiter().GetResult();

                    if (result.responseCode == "000")
                    {
                        cVerified.Checked = true;
                    }
                    else
                    {
                        if (result.matchPercentate < 80)
                        {
                            Program.SBO_Application.MessageBox($"{result.responseMessage}");
                            return;
                        }
                        else
                        {
                            StringBuilder message = new StringBuilder();
                            message.AppendLine("Warning");
                            message.AppendLine($"{result.responseMessage}");
                            message.AppendLine("Do you want to continue?");
                            var input = Program.SBO_Application.MessageBox(message.ToString(), 1, "Yes", "No");

                            if (input == 2)
                                return;
                        }
                    }
                    DisplayTransactionConfirmation();
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void bProcess_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                Process().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void BtnAdd_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE && chBank.Checked)
            {
                if (!ValidateAddButton())
                {
                    BubbleEvent = false;
                    return;
                }
            }
        }

        private void chBank_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CheckBank(chBank.Checked);
        }

        private void bAdd_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            FormSetting();
        }

        private void TxtGLAccount_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (chBank.Checked && pVal.ColUID == "8")
            {
                Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                string query = $@"SELECT T0.""U_ACCTCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""U_ISENABLE"" = 'True' AND IFNULL(T0.""U_ACCTCODE"",'') <> ''";
                rec.DoQuery(query);
                if (rec.RecordCount > 0)
                {
                    try
                    {
                        int index = 1;
                        SAPbouiCOM.ChooseFromList oCFLEvento = default(SAPbouiCOM.ChooseFromList);
                        SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                        SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);

                        var cflList = UIAPIRawForm.ChooseFromLists;

                        oCFLEvento = this.UIAPIRawForm.ChooseFromLists.Item("2");

                        oCFLEvento.SetConditions(null);
                        oCons = oCFLEvento.GetConditions();
                        while (!rec.EoF)
                        {
                            oCon = oCons.Add();
                            oCon.Alias = "AcctCode";
                            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            oCon.CondVal = rec.Fields.Item(0).Value.ToString();
                            if (index != rec.RecordCount)
                                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;
                            rec.MoveNext();
                            index++;
                        }
                        oCFLEvento.SetConditions(oCons);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }


        private void FormSetting()
        {
            CheckBank(false);

            chBank.Item.Top = LblWTax.Item.Top + LblWTax.Item.Height;
            chBank.Item.Left = LblWTax.Item.Left;

            StaticText0.Item.Top = LblCurr.Item.Top + LblCurr.Item.Height;
            StaticText0.Item.Left = LblCurr.Item.Left;

            cbType.Item.Top = TxtCurr.Item.Top + TxtCurr.Item.Height;
            cbType.Item.Left = TxtCurr.Item.Left;

            StaticText1.Item.Top = LblCurr.Item.Top + LblCurr.Item.Height * 2;
            StaticText1.Item.Left = LblCurr.Item.Left;

            cbCAT.Item.Top = TxtCurr.Item.Top + TxtCurr.Item.Height * 2;
            cbCAT.Item.Left = TxtCurr.Item.Left;

            StaticText2.Item.Top = LblCurr.Item.Top + LblCurr.Item.Height * 3;
            StaticText2.Item.Left = LblCurr.Item.Left;

            tBat.Item.Top = TxtCurr.Item.Top + TxtCurr.Item.Height * 3;
            tBat.Item.Left = TxtCurr.Item.Left;

            bPay.Item.Top = BtnCancel.Item.Top;
            bPay.Item.Left = BtnCancel.Item.Left + BtnCancel.Item.Width + 7;

            bProcess.Item.Top = BtnCancel.Item.Top + 500;
        }

        private void CheckBank(bool isChecked)
        {
            try
            {
                txtCC.Item.Click();
            }
            catch (Exception) { }

            StaticText0.Item.Visible = isChecked;
            cbCAT.Item.Visible = isChecked;
            StaticText1.Item.Visible = isChecked;
            cbType.Item.Visible = isChecked;
            StaticText2.Item.Visible = isChecked;
            tBat.Item.Visible = isChecked;

            if (!isChecked)
            {
                cbType.Select("", SAPbouiCOM.BoSearchKey.psk_ByValue);
                cbCAT.Select("", SAPbouiCOM.BoSearchKey.psk_ByValue);
            }

            bPay.Item.Visible = isChecked;
        }

        private void SetSenderDetails()
        {
            string query = $@"SELECT TOP 1 T0.""U_ACCOUNTNUM"", T0.""U_ACCOUNTNAME"", T0.""U_BANKNAME"", T0.""U_BANKCODE"", T0.""U_BRANCHCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""U_ISENABLE"" = 'True' AND T0.""U_ACCTCODE"" = '{TxtGLA.Value}'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            if (rec.RecordCount > 0)
            {
                sender = new BankAccount()
                {
                    AccountNo = rec.Fields.Item(0).Value.ToString(),
                    AccountName = rec.Fields.Item(1).Value.ToString(),
                    BankName = rec.Fields.Item(2).Value.ToString(),
                    BankCode = rec.Fields.Item(3).Value.ToString(),
                    BranchCode = rec.Fields.Item(4).Value.ToString()
                };
            }
        }

        private void SetVendorDetails()
        {
            var vendorCode = txtCC.Value;
            string query = $@"SELECT TOP 1 T1.""AcctName"", T1.""Account"", T2.""BankName"", T1.""MandateID"", T1.""Branch"" FROM OCRD T0  INNER JOIN OCRB T1 ON T0.""CardCode"" = T1.""CardCode"" INNER JOIN ODSC T2 ON T0.""BankCode"" = T2.""BankCode"" INNER JOIN DSC1 T3 ON T0.""BankCode"" = T3.""BankCode"" WHERE T0.""CardCode"" = '{vendorCode}'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            if (rec.RecordCount > 0)
            {
                receiver = new BankAccount()
                {
                    AccountName = rec.Fields.Item(0).Value.ToString(),
                    AccountNo = rec.Fields.Item(1).Value.ToString(),
                    BankName = rec.Fields.Item(2).Value.ToString(),
                    BankCode = rec.Fields.Item(3).Value.ToString(),
                    BranchCode = rec.Fields.Item(4).Value.ToString()
                };
            }
        }

        private void SetReceiverDetails()
        {
            var GLAccount = Matrix0.GetCellValue("8", 1).ToString();

            string query = $@"SELECT TOP 1 T0.""U_ACCOUNTNAME"", T0.""U_ACCOUNTNUM"", T0.""U_BANKNAME"", T0.""U_BANKCODE"", T0.""U_BRANCHCODE"" FROM ""HIM_06_15"".""@NCHL_BANK""  T0 WHERE T0.""U_ACCTCODE"" = '{GLAccount}' AND T0.""U_ISENABLE"" = 'True'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            if (rec.RecordCount > 0)
            {
                receiver = new BankAccount()
                {
                    AccountName = rec.Fields.Item(0).Value.ToString(),
                    AccountNo = rec.Fields.Item(1).Value.ToString(),
                    BankName = rec.Fields.Item(2).Value.ToString(),
                    BankCode = rec.Fields.Item(3).Value.ToString(),
                    BranchCode = rec.Fields.Item(4).Value.ToString()
                };
            }
        }

        private bool ValidateTransferAmount()
        {
            string transactionType = cbType.Value;

            var isValid = transactionType == "RT" ?
                        new NchlCipsService().ValidateTransferAmount(amount, sender.BankCode == receiver.BankCode, out string errMessage) :
                        new NchlIpsService().ValidateTransferAmount(amount, sender.BankCode == receiver.BankCode, out errMessage);
            if (!isValid)
                Program.SBO_Application.StatusBar.SetText(errMessage, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            return isValid;
        }

        private void DisplayTransactionConfirmation()
        {
            var realTimeService = new NchlCipsService();
            var chargeAmount = realTimeService.GetChargeAmount(amount, sender.BankName == receiver.BankName);
            var tranConfirmModel = new TransactionConfirmationModel()
            {
                SenderBank = sender.BankName,
                SenderAcctNumber = sender.AccountNo,
                SenderAcctName = sender.AccountName,

                TransactionDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"),
                ReferenceID = $"ACCOUNTFT:{sender.AccountName}",
                TransactionDetail = txtRemarks.Value,
                CreditorBank = receiver.BankName,
                CreditorAcctName = receiver.AccountName,
                CreditorAcctNum = receiver.AccountNo,
                TransactionAmt = amount.ToString(),
                ChargeAmt = chargeAmount.ToString(),
                TotalAmt = (amount + chargeAmount).ToString()
            };

            var confirmationForm = new TransactionConfimation(UIAPIRawForm.Type, UIAPIRawForm.TypeCount, tranConfirmModel);
            confirmationForm.Show();
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

        private bool ValidatePayButton()
        {
            try
            {
                if (chPay.Checked)
                {
                    Program.SBO_Application.StatusBar.SetText("Already paid.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (OptionVendor.Selected && string.IsNullOrEmpty(txtCC.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please select the vendor.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (OptionVendor.Selected && string.IsNullOrEmpty(txtDUE.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the amount.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (OptionAccount.Selected && string.IsNullOrEmpty(TxtAcctAmount.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the amount.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (!chBank.Checked)
                {
                    Program.SBO_Application.StatusBar.SetText("Please check the NCHL-NPI (Bank Integration).", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (string.IsNullOrEmpty(cbType.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please select the Transaction Type.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (string.IsNullOrEmpty(cbCAT.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please select the Category Purpose.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtRemarks.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the remarks.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task Process()
        {
            string transactionType = cbType.Value;

            CipsBatchResponseModel response = new CipsBatchResponseModel();
            if (transactionType == "RT")
                response = await RealTimeTransferAsync();
            else
                response = await NonRealTimeTransferAsync();

            TxID.Value = response.cipsBatchResponse.id.ToString();
            TxCode.Value = response.cipsBatchResponse.responseCode;
            TxMsg.Value = response.cipsBatchResponse.responseMessage ?? "";

            if (response.cipsBatchResponse.responseCode == "000")
            {
                chPay.Checked = true;
                Program.SBO_Application.StatusBar.SetText($"{response.cipsBatchResponse.responseMessage}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                bAdd.Item.Click();
            }
            else
            {
                chPay.Checked = false;
                //Program.SBO_Application.MessageBox($"{response.cipsBatchResponse.responseMessage}");                    
                Program.SBO_Application.StatusBar.SetText($"payment process failed.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private bool ValidateAddButton()
        {
            if (!chPay.Checked)
            {
                Program.SBO_Application.StatusBar.SetText("Please proceed the payment first.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private async Task<ValidateBankAccountResponse> ValidateAccountAsync()
        {
            var bankAccount = new ValidateBankAccount()
            {
                bankId = receiver.BankCode,
                accountId = receiver.AccountNo,
                accountName = receiver.AccountName
            };
            var bankAccountService = new BankAccountService();
            var response = await bankAccountService.ValidateAccount(bankAccount);
            return response;
        }

        private async Task<CipsBatchResponseModel> RealTimeTransferAsync()
        {
            var data = GetRealTimeData();
            var realTimeService = new NchlCipsService(data);
            CipsBatchResponseModel response = await realTimeService.SendTransactionAsync();
            return response;
        }

        private NchlCIpsTransaction GetRealTimeData()
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

            var realTime = new NchlCIpsTransaction
            {
                cipsBatchDetail = cipsBatchDetail
            };
            realTime.cipsTransactionDetailList.Add(cipsTransactionDetailList);

            return realTime;
        }

        private async Task<CipsBatchResponseModel> NonRealTimeTransferAsync()
        {
            var data = GetNonRealTimeData();
            var nonRealtimeService = new NchlIpsService(data);
            CipsBatchResponseModel response = await nonRealtimeService.SendTransactionAsync();
            return response;
        }

        private NchlIpsTransaction GetNonRealTimeData()
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

            var realTime = new NchlIpsTransaction
            {
                nchlIpsBatchDetail = cipsBatchDetail
            };
            realTime.nchlIpsTransactionDetailList.Add(cipsTransactionDetailList);

            return realTime;
        }
    }
}
