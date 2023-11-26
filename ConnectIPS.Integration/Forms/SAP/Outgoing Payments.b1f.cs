using ConnectIPS.Integration.Forms.Users;
using ConnectIPS.Integration.Services;
using MainLibrary.SAPB1;
using NepalPay.Library.Credentials;
using NepalPay.Library.Models.Account;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;
using System.Xml.Linq;

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
            this.chNCHL = ((SAPbouiCOM.CheckBox)(this.GetItem("chNCHL").Specific));
            this.chNCHL.PressedAfter += new SAPbouiCOM._ICheckBoxEvents_PressedAfterEventHandler(this.chBank_PressedAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.cbType = ((SAPbouiCOM.ComboBox)(this.GetItem("cbType").Specific));
            this.chStatus = ((SAPbouiCOM.CheckBox)(this.GetItem("chStatus").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.tBat = ((SAPbouiCOM.EditText)(this.GetItem("tBat").Specific));
            this.bAdd = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.bAdd.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.bAdd_PressedAfter);
            this.bAdd.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.BtnAdd_ClickBefore);
            this.txtCC = ((SAPbouiCOM.EditText)(this.GetItem("5").Specific));
            this.txtDN = ((SAPbouiCOM.EditText)(this.GetItem("3").Specific));
            this.txtPaymentOnAccount = ((SAPbouiCOM.EditText)(this.GetItem("13").Specific));
            this.txtRemarks = ((SAPbouiCOM.EditText)(this.GetItem("26").Specific));
            this.LblWTax = ((SAPbouiCOM.StaticText)(this.GetItem("151").Specific));
            this.TxtWTax = ((SAPbouiCOM.EditText)(this.GetItem("152").Specific));
            this.LblCurr = ((SAPbouiCOM.StaticText)(this.GetItem("23").Specific));
            this.TxtCurr = ((SAPbouiCOM.EditText)(this.GetItem("21").Specific));
            this.BtnCancel = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.OptionVendor = ((SAPbouiCOM.OptionBtn)(this.GetItem("57").Specific));
            this.OptionVendor.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.OptionVendor_PressedAfter);
            this.OptionAccount = ((SAPbouiCOM.OptionBtn)(this.GetItem("58").Specific));
            this.OptionAccount.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.OptionAccount_PressedAfter);
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
            this.chValid = ((SAPbouiCOM.CheckBox)(this.GetItem("chVal").Specific));
            this.tDraftKey = ((SAPbouiCOM.EditText)(this.GetItem("tDraftKey").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.txtTotal = ((SAPbouiCOM.EditText)(this.GetItem("txtTotal").Specific));
            this.OptionBtn0 = ((SAPbouiCOM.OptionBtn)(this.GetItem("56").Specific));
            this.OptionBtn0.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.OptionBtn0_PressedAfter);
            this.OptionBtn1 = ((SAPbouiCOM.OptionBtn)(this.GetItem("10002011").Specific));
            this.OptionBtn1.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.OptionBtn1_PressedAfter);
            this.OptionBtn2 = ((SAPbouiCOM.OptionBtn)(this.GetItem("140002003").Specific));
            this.OptionBtn2.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.OptionBtn2_PressedAfter);
            this.cbBranch = ((SAPbouiCOM.ComboBox)(this.GetItem("1320002037").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataAddAfter += new SAPbouiCOM.Framework.FormBase.DataAddAfterHandler(this.Form_DataAddAfter);
            this.DataAddBefore += new DataAddBeforeHandler(this.Form_DataAddBefore);
        }

        private void OnCustomInitialize()
        {
            var oCFLEvento = this.UIAPIRawForm.ChooseFromLists.Item("1");
            BasicBinding();
        }

        private SAPbouiCOM.Button bAdd;
        private SAPbouiCOM.EditText txtCC;
        private SAPbouiCOM.EditText txtDN;
        private SAPbouiCOM.EditText txtPaymentOnAccount;
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
        private SAPbouiCOM.OptionBtn OptionBtn0;
        private SAPbouiCOM.OptionBtn OptionBtn1;
        private SAPbouiCOM.OptionBtn OptionBtn2;
        private SAPbouiCOM.ComboBox cbBranch;

        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.ComboBox cbCAT;
        private SAPbouiCOM.CheckBox chNCHL;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.ComboBox cbType;
        private SAPbouiCOM.CheckBox chStatus;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText tBat;
        private SAPbouiCOM.EditText TxID;
        private SAPbouiCOM.EditText TxCode;
        private SAPbouiCOM.EditText TxMsg;
        private SAPbouiCOM.CheckBox chValid;
        private SAPbouiCOM.Button bDetail;
        private SAPbouiCOM.EditText tDraftKey;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.EditText txtTotal;

        private double amount;
        private BankAccount sender;
        private BankAccount receiver;
        private string draftKey;

        private NchlNpiResponse NCHL_transfer()
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
            return response;
        }

        private void BtnAdd_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {

                ////NCHL Detail Validation
                //if (chNCHL.Checked && !ValidateNCHL())
                //{
                //    BubbleEvent = false;
                //    return;
                //}

                ////NCHL After Click
                //InitializeNCHL();
                //string remarks = txtRemarks.Value;
                //string transactionType = cbType.Value;

                //NCHL_Details form = new NCHL_Details(sender, receiver, amount, transactionType, remarks, UIAPIRawForm.Type, UIAPIRawForm.TypeCount);
                //form.Show();

                string draftKey = UIAPIRawForm.DataSources.DBDataSources.Item("OVPM").GetValue("DocEntry", 0).ToString();
                if (!string.IsNullOrEmpty(draftKey))
                    tDraftKey.Value = draftKey;

                if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE && chNCHL.Checked && !ValidateAdding())
                {
                    BubbleEvent = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText("Error occurs due to: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void chBank_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CheckNCHL(chNCHL.Checked);
        }

        private void bAdd_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            BasicBinding();
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
                    //if (pVal.ColUID == "8")
                    //{
                    //    Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    //    string query = $@"SELECT T0.""U_ACCTCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""U_ISENABLE"" = 'True' AND IFNULL(T0.""U_ACCTCODE"",'') <> ''";
                    //    rec.DoQuery(query);
                    //    if (rec.RecordCount > 0)
                    //    {
                    //        int index = 1;
                    //        while (!rec.EoF)
                    //        {
                    //            oCon = oCons.Add();
                    //            oCon.Alias = "AcctCode";
                    //            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    //            oCon.CondVal = rec.Fields.Item(0).Value.ToString();
                    //            if (index != rec.RecordCount)
                    //                oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;
                    //            rec.MoveNext();
                    //            index++;
                    //        }
                    //    }
                    //}
                }
                oCFLEvento.SetConditions(oCons);
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void Form_DataAddBefore(ref SAPbouiCOM.BusinessObjectInfo pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            draftKey = UIAPIRawForm.DataSources.DBDataSources.Item("OVPM").GetValue("DocEntry", 0).ToString();
        }

        private void OptionBtn0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            chNCHL.Item.Visible = false;
        }

        private void OptionVendor_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            chNCHL.Item.Visible = true;
        }

        private void OptionAccount_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            chNCHL.Item.Visible = true;
        }

        private void OptionBtn1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            chNCHL.Item.Visible = false;
        }

        private void OptionBtn2_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            chNCHL.Item.Visible = false;
        }

        private void bDetail_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (chNCHL.Checked && !ValidateNCHL())
                    BubbleEvent = false;
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

        private void Form_DataAddAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            try
            {
                if (pVal.ActionSuccess)
                {
                    if (chNCHL.Checked && !string.IsNullOrEmpty(draftKey) && CheckApproval(draftKey))
                    {
                        var response = NCHL_transfer();

                        var xmlString = pVal.ObjectKey;
                        XDocument xdoc = XDocument.Parse(xmlString);
                        string strdocEntry = xdoc.Root.Element("DocEntry").Value;
                        int.TryParse(strdocEntry, out int docEntry);

                        if (response.cipsBatchResponse.responseCode == "000")
                        {
                            string query = $@"UPDATE OVPM T0 SET T0.U_NCHLPAYMENT  = 'Y', T0.U_NCHLID = '{response.cipsBatchResponse.id.ToString()}', T0.U_NCHLCODE = '{response.cipsBatchResponse.responseCode}', T0.U_NCHLMSG = '{response.cipsBatchResponse.responseMessage ?? ""}' WHERE T0.""DocEntry""  = {docEntry}";
                            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                            rec.DoQuery(query);
                            Program.SBO_Application.StatusBar.SetText($"{response.cipsBatchResponse.responseMessage}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                            //Incoming Payment
                            if (OptionAccount.Selected)
                            {
                                var SumPaid = Convert.ToDouble(txtTotal.Value);
                                //var TransferAccount1 = TxtGLA.Value;
                                var TransferSum = Convert.ToDouble(txtTotal.Value);

                                //one
                                var AccountCode1 = Matrix0.GetCellValue("8", 1).ToString();

                                query = $@"SELECT T0.""BPLId"" FROM OBPL T0 WHERE T0.""U_BFL"" = '{AccountCode1}'";
                                Recordset Rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                                Rec.DoQuery(query);
                                var branch = Convert.ToInt32(Rec.Fields.Item(0).Value);

                                var NCHLAcct = Matrix0.GetCellValue("U_NCHLACCOUNTNUM", 1).ToString();
                                query = $@"SELECT TOP 1 T0.""U_ACCTCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""Code"" = '{NCHLAcct}' AND T0.""U_ISENABLE"" = 'True'";
                                Rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                                Rec.DoQuery(query);
                                var TransferAccount = Rec.Fields.Item(0).Value.ToString();

                                //two
                                var Branch = Convert.ToInt32(cbBranch.Value.Trim());
                                query = $@"SELECT T0.""U_BFL"" FROM OBPL T0 WHERE T0.""BPLId"" = '{Branch}'";
                                Rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                                Rec.DoQuery(query);
                                var accountCode = Rec.Fields.Item(0).Value.ToString();

                                IncomingPaymentParams obj = new IncomingPaymentParams()
                                {
                                    Branch = branch,

                                    TransferAccount = TransferAccount,
                                    TransferSum = Convert.ToDouble(txtTotal.Value),

                                    AccountCode = accountCode,
                                    SumPaid = Convert.ToDouble(txtTotal.Value)
                                };
                                PaymentService.AddIncomingPayment(obj);
                            }
                        }
                        else
                        {
                            string query = $@"UPDATE OVPM T0 SET T0.U_NCHLPAYMENT  = 'N', T0.U_NCHLID = '{response.cipsBatchResponse.id.ToString()}', T0.U_NCHLCODE = '{response.cipsBatchResponse.responseCode}', T0.U_NCHLMSG = '{response.cipsBatchResponse.responseMessage ?? ""}' WHERE T0.""DocEntry""  = {docEntry}";
                            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                            rec.DoQuery(query);

                            //Program.SBO_Application.MessageBox($"{response.cipsBatchResponse.responseMessage}");                    
                            Program.SBO_Application.StatusBar.SetText($"payment process failed.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.StatusBar.SetText($"Error occurs due to: {ex.Message}", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }


        private BankAccount SetSenderDetails(string GLAccount)
        {
            string query = $@"SELECT TOP 1 T0.""Code"", T0.""Name"", T0.""U_BANKNAME"", T0.""U_BANKCODE"", T0.""U_BRANCHCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""U_ISENABLE"" = 'True' AND T0.""U_ACCTCODE"" = '{TxtGLA.Value}'";
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
            string query = $@"SELECT TOP 1 T1.""AcctName"", T1.""Account"", T2.""BankName"", T1.""MandateID"", T1.""Branch"" FROM OCRD T0  INNER JOIN OCRB T1 ON T0.""CardCode"" = T1.""CardCode"" INNER JOIN ODSC T2 ON T0.""BankCode"" = T2.""BankCode"" WHERE T0.""CardCode"" = '{vendorCode}'";
            //string query = $@"SELECT TOP 1 T1.""AcctName"", T1.""Account"", T1.""MandateID"", T1.""Branch"" FROM OCRD T0  INNER JOIN OCRB T1 ON T0.""CardCode"" = T1.""CardCode"" WHERE T0.""CardCode"" = '{vendorCode}'";
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

        private BankAccount SetReceiverDetails(string accountNumber)
        {
            //string query = $@"SELECT TOP 1 T0.""Code"", T0.""Name"", T0.""U_BANKNAME"", T0.""U_BANKCODE"", T0.""U_BRANCHCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""Code"" = '{accountNumber}' AND T0.""U_ISENABLE"" = 'True'";
            string query = $@"SELECT TOP 1 T0.""Code"", T0.""Name"", T0.""U_BANKNAME"", T0.""U_BANKCODE"", T0.""U_BRANCHCODE"" FROM ""@NCHL_BANK""  T0 WHERE T0.""Code"" = '{accountNumber}' AND T0.""U_ISENABLE"" = 'True'";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            BankAccount receiver = null;
            if (rec.RecordCount > 0)
            {
                receiver = new BankAccount()
                {
                    AccountNo = rec.Fields.Item(0).Value.ToString(),
                    AccountName = rec.Fields.Item(1).Value.ToString(),
                    BankName = rec.Fields.Item(2).Value.ToString(),
                    BankCode = rec.Fields.Item(3).Value.ToString(),
                    BranchCode = rec.Fields.Item(4).Value.ToString()
                };
            }
            return receiver;
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

        private void BasicBinding()
        {
            CheckNCHL(chNCHL.Checked);
            FormSetting();
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

            //StaticText2.Item.Top = LblCurr.Item.Top + LblCurr.Item.Height * 3;
            //StaticText2.Item.Left = LblCurr.Item.Left;

            //tBat.Item.Top = TxtCurr.Item.Top + TxtCurr.Item.Height * 3;
            //tBat.Item.Left = TxtCurr.Item.Left;

            bDetail.Item.Top = BtnCancel.Item.Top;
            bDetail.Item.Left = BtnCancel.Item.Left + BtnCancel.Item.Width + 7;
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

        private string GetBatch()
        {
            var randomNum = (new Random()).Next(100, 1000);
            var batchId = $"{NPICredential.BatchPrefix}-{txtDN.Value}-{randomNum}";
            tBat.Value = batchId;
            return batchId;
        }

        private string GetInstructionId()
        {
            var InstructionId = $"{GetBatch()}-1";
            return InstructionId;
        }

        private void InitializeNCHL()
        {
            string senderGLAccount = TxtGLA.Value;
            sender = SetSenderDetails(senderGLAccount);

            if (OptionVendor.Selected)
            {
                var vendorCode = txtCC.Value;
                receiver = SetVendorDetails(vendorCode);
            }
            else if (OptionAccount.Selected)
            {
                //var receiverGLAccount = Matrix0.GetCellValue("8", 1).ToString();
                var nchlAccountNumber = Matrix0.GetCellValue("U_NCHLACCOUNTNUM", 1).ToString();
                receiver = SetReceiverDetails(nchlAccountNumber);
            }

            amount = Convert.ToDouble(txtTotal.Value);
        }

        private bool ValidateNCHL()
        {
            if (OptionVendor.Selected)
            {
                if (string.IsNullOrEmpty(txtCC.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please select the Vendor.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (txtPaymentOnAccount.Value == "0.0")
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the Payment on Account.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtRemarks.Value))
                {
                    Program.SBO_Application.StatusBar.SetText("Please enter the Remarks.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return false;
                }
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
            else if (string.IsNullOrEmpty(TxtGLA.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select the G/L Account on Payment Means.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(txtTotal.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select the Total on Payment Means.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        //note: improve logic
        private bool CheckApproval(string draftKey)
        {
            string query = $@"SELECT T0.""WddStatus"" FROM OPDF T0 WHERE T0.""ObjType"" = 46 AND T0.""DocEntry"" = {draftKey}  ";
            Recordset rec = (Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery(query);
            var approvalStatus = rec.Fields.Item(0).Value.ToString();

            if (approvalStatus == "-" || approvalStatus == "Y")
                return true;
            else
                return false;
        }
    }
}
