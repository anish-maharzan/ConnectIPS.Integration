using ConnectIPS.Integration.Forms.Users;
using ConnectIPS.Integration.Services;
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
            this.chSender = ((SAPbouiCOM.CheckBox)(this.GetItem("chSender").Specific));
            this.chNCHL = ((SAPbouiCOM.CheckBox)(this.GetItem("chNCHL").Specific));
            this.chNCHL.PressedAfter += new SAPbouiCOM._ICheckBoxEvents_PressedAfterEventHandler(this.chBank_PressedAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.cbType = ((SAPbouiCOM.ComboBox)(this.GetItem("cbType").Specific));
            this.bProcess = ((SAPbouiCOM.Button)(this.GetItem("bProc").Specific));
            this.bProcess.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bProcess_ClickAfter);
            this.chStatus = ((SAPbouiCOM.CheckBox)(this.GetItem("chStatus").Specific));
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
            this.bDetail = ((SAPbouiCOM.Button)(this.GetItem("bDetail").Specific));
            this.bDetail.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.bDetail_ClickBefore);
            this.bDetail.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bDetail_ClickAfter);
            this.chReceiver = ((SAPbouiCOM.CheckBox)(this.GetItem("chReceiver").Specific));
            this.chValid = ((SAPbouiCOM.CheckBox)(this.GetItem("chVal").Specific));
            this.bStatus = ((SAPbouiCOM.Button)(this.GetItem("bStatus").Specific));
            this.CheckBox2 = ((SAPbouiCOM.CheckBox)(this.GetItem("chAmt").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataAddAfter += new DataAddAfterHandler(this.Form_DataAddAfter);
        }

        private void OnCustomInitialize()
        {
            BasicBinding();
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
        private SAPbouiCOM.CheckBox chSender;
        private SAPbouiCOM.Button bPay;
        private SAPbouiCOM.CheckBox chNCHL;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.ComboBox cbType;
        private SAPbouiCOM.Button bProcess;
        private SAPbouiCOM.CheckBox chStatus;
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
            try
            {
                if (!ValidateNCHL())
                {
                    BubbleEvent = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void bPay_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                string transactionType = cbType.Value;
                InitializeNCHL();
                NchlNpiResponse response = new NchlNpiResponse();
                if (transactionType == "RT")
                {
                    var data = GetRealTimeData();
                    response = NchlService.RealTimeTransfer(data);
                }
                else
                {
                    var data = GetNonRealTimeData();
                    response = NchlService.NonRealTimeTransfer(data);
                }

                TxID.Value = response.cipsBatchResponse.id.ToString();
                TxCode.Value = response.cipsBatchResponse.responseCode;
                TxMsg.Value = response.cipsBatchResponse.responseMessage ?? "";

                if (response.cipsBatchResponse.responseCode == "000")
                {
                    chStatus.Checked = true;
                    Program.SBO_Application.StatusBar.SetText($"{response.cipsBatchResponse.responseMessage}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    bAdd.Item.Click();
                }
                else
                {
                    chStatus.Checked = false;
                    //Program.SBO_Application.MessageBox($"{response.cipsBatchResponse.responseMessage}");                    
                    Program.SBO_Application.StatusBar.SetText($"payment process failed.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void bProcess_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //try
            //{
            //    Process().GetAwaiter().GetResult();
            //}
            //catch (Exception ex)
            //{
            //    Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            //}
        }

        private void BtnAdd_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE && chNCHL.Checked && !CheckApproval())
            {
                if (!ValidateAdding())
                {
                    BubbleEvent = false;
                    return;
                }
            }
        }

        private bool ValidateAdding()
        {
            if (!chValid.Checked)
            {
                Program.SBO_Application.StatusBar.SetText("Please verify NCHL details.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private void chBank_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CheckNCHL(chNCHL.Checked);
        }

        private void bAdd_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            BasicBinding();
        }

        private void BasicBinding()
        {
            CheckNCHL(chNCHL.Checked);
            FormSetting();
            ShowPayButton();
            ShowStatusButton();
        }

        private void TxtGLAccount_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                SAPbouiCOM.ChooseFromList oCFLEvento = default(SAPbouiCOM.ChooseFromList);
                SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);
                oCFLEvento = this.UIAPIRawForm.ChooseFromLists.Item("2");
                oCFLEvento.SetConditions(null);
                oCons = oCFLEvento.GetConditions();
                if (chNCHL.Checked && OptionAccount.Selected)
                {
                    if (pVal.Row > 1)
                    {
                        Program.SBO_Application.StatusBar.SetText("Single Line is allowed for NCHL-NPI (Bank Integration)", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        BubbleEvent = false;
                        return;
                    }
                    if (pVal.ColUID == "8")
                    {
                        Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                        string query = $@"SELECT T0.""U_ACCTCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""U_ISENABLE"" = 'True' AND IFNULL(T0.""U_ACCTCODE"",'') <> ''";
                        rec.DoQuery(query);
                        if (rec.RecordCount > 0)
                        {
                            int index = 1;
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
                        }
                    }
                }
                oCFLEvento.SetConditions(oCons);
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void FormSetting()
        {
            chNCHL.Item.Top = LblWTax.Item.Top + LblWTax.Item.Height;
            chNCHL.Item.Left = LblWTax.Item.Left;

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

            bDetail.Item.Top = BtnCancel.Item.Top;
            bDetail.Item.Left = BtnCancel.Item.Left + BtnCancel.Item.Width + 7;

            bStatus.Item.Top = bDetail.Item.Top;
            bStatus.Item.Left = bDetail.Item.Left + bDetail.Item.Width + 7;

            bProcess.Item.Top = BtnCancel.Item.Top + 500;
        }

        private void CheckNCHL(bool isChecked)
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

            bDetail.Item.Visible = isChecked;
        }

        private BankAccount SetSenderDetails(string GLAccount)
        {
            string query = $@"SELECT TOP 1 T0.""U_ACCOUNTNUM"", T0.""U_ACCOUNTNAME"", T0.""U_BANKNAME"", T0.""U_BANKCODE"", T0.""U_BRANCHCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""U_ISENABLE"" = 'True' AND T0.""U_ACCTCODE"" = '{TxtGLA.Value}'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);

            BankAccount sender = null;
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
            return sender;
        }

        private BankAccount SetVendorDetails(string vendorCode)
        {
            string query = $@"SELECT TOP 1 T1.""AcctName"", T1.""Account"", T2.""BankName"", T1.""MandateID"", T1.""Branch"" FROM OCRD T0  INNER JOIN OCRB T1 ON T0.""CardCode"" = T1.""CardCode"" INNER JOIN ODSC T2 ON T0.""BankCode"" = T2.""BankCode"" INNER JOIN DSC1 T3 ON T0.""BankCode"" = T3.""BankCode"" WHERE T0.""CardCode"" = '{vendorCode}'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            BankAccount receiver = null;
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
            return receiver;
        }

        private BankAccount SetReceiverDetails(string GLAccount)
        {
            string query = $@"SELECT TOP 1 T0.""U_ACCOUNTNAME"", T0.""U_ACCOUNTNUM"", T0.""U_BANKNAME"", T0.""U_BANKCODE"", T0.""U_BRANCHCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""U_ACCTCODE"" = '{GLAccount}' AND T0.""U_ISENABLE"" = 'True'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            BankAccount receiver = null;
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

            return receiver;
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

        private CIpsTransaction GetRealTimeData()
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

            var realTime = new CIpsTransaction
            {
                cipsBatchDetail = cipsBatchDetail
            };
            realTime.cipsTransactionDetailList.Add(cipsTransactionDetailList);

            return realTime;
        }

        private NchlIpsTransaction GetNonRealTimeData()
        {
            var cipsBatchDetail = new NepalPay.Library.Models.Transaction.NchlIpsBatchDetail()
            {
                batchId = GetBatch(),
                batchAmount = amount.ToString(),
                debtorAgent = sender.BankCode,
                debtorBranch = sender.BranchCode,
                debtorName = sender.AccountName,
                debtorAccount = sender.AccountNo,
                categoryPurpose = cbCAT.Value
            };

            var cipsTransactionDetailList = new NepalPay.Library.Models.Transaction.NchlIpsTransactionDetail()
            {
                instructionId = GetInstructionId(),
                endToEndId = txtRemarks.Value,
                amount = amount.ToString(),
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

        private SAPbouiCOM.Button bDetail;

        private void bDetail_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (chNCHL.Checked)
                {
                    if (!ValidateNCHL())
                    {
                        BubbleEvent = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText("Error occurs due to: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void bDetail_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                InitializeNCHL();
                string remarks = txtRemarks.Value;
                string transactionType = cbType.Value;

                NCHL_Details form = new NCHL_Details(sender, receiver, amount, transactionType, remarks, UIAPIRawForm.Type, UIAPIRawForm.TypeCount);
                form.Show();
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText("Error occurs due to: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void InitializeNCHL()
        {
            string senderGLAccount = TxtGLA.Value;
            sender = SetSenderDetails(senderGLAccount);

            if (OptionVendor.Selected)
            {
                var vendorCode = txtCC.Value;
                receiver = SetVendorDetails(vendorCode);

                var amountString = txtDUE.Value.Split(' ')[1];
                double.TryParse(amountString, out amount);
            }
            else if (OptionAccount.Selected)
            {
                var receiverGLAccount = Matrix0.GetCellValue("8", 1).ToString();
                receiver = SetReceiverDetails(receiverGLAccount);

                var amountString = TxtAcctAmount.Value.Split(' ')[1];
                double.TryParse(amountString, out amount);
            }
        }

        private bool ValidateNCHL()
        {
            if (OptionAccount.Selected || OptionAccount.Selected)
            {
                Program.SBO_Application.StatusBar.SetText("Only Vendor or Account are allowed for NCHL-NPI (Bank Integration).", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }

            if (OptionVendor.Selected)
            {
                if (string.IsNullOrEmpty(txtCC.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please select the Vendor.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtDUE.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the Amount.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtRemarks.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the Remarks.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                //else if (string.IsNullOrEmpty(TxtGLA.Value))
                //{
                //    Program.SBO_Application.StatusBar.SetText("Please select the Payment Means.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                //    return false;
                //}
                else
                    return true;
            }
            else if (OptionAccount.Selected)
            {
                if (string.IsNullOrEmpty(TxtAcctAmount.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the Amount.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                if (string.IsNullOrEmpty(txtRemarks.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the Remarks.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else
                    return true;
            }
            if (string.IsNullOrEmpty(cbType.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select the Transaction Type.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(cbCAT.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select the Transaction Fee.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private void ShowPayButton()
        {
            bool isApproved = CheckApproval();
            if (isApproved)
            {
                bPay.Item.Visible = true;

                bPay.Item.Top = BtnCancel.Item.Top;
                bPay.Item.Left = bDetail.Item.Left + bDetail.Item.Width + 7;
            }
            else
            {
                bPay.Item.Visible = false;

                bPay.Item.Top = BtnCancel.Item.Top + 1000;
                bPay.Item.Left = bDetail.Item.Left + bDetail.Item.Width + 7;
            }
        }

        private bool CheckApproval()
        {
            string docEntry = UIAPIRawForm.DataSources.DBDataSources.Item("OVPM").GetValue("DocEntry", 0).ToString();
            if (string.IsNullOrEmpty(docEntry))
                return false;

            int userSign = Program.oCompany.UserSignature;

            string query = $@"SELECT T0.""WddStatus"" FROM OPDF T0  INNER JOIN OUSR T1 ON T0.""UserSign"" = T1.""USERID"" WHERE T0.""ObjType"" = 46 AND T1.""USERID"" = {userSign} AND T0.""DocEntry"" = {docEntry}  ";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            var approvalStatus = rec.Fields.Item(0).Value.ToString();

            if (approvalStatus == "Y" || approvalStatus == "")
                return true;
            else
                return false;
        }

        private void ShowStatusButton()
        {
            bStatus.Item.Visible = false;
        }

        private SAPbouiCOM.CheckBox chReceiver;
        private SAPbouiCOM.CheckBox chValid;
        private SAPbouiCOM.Button bStatus;

        private void Form_DataAddAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            try
            {
                if (pVal.ActionSuccess)
                {
                    var isApproved = CheckApproval();
                    bPay.Item.Click();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private SAPbouiCOM.CheckBox CheckBox2;
    }
}
