using ConnectIPS.Integration.Forms.Users;
using NepalPay.Library.Models.QR;
using NepalPay.Library.Services.Implementation;
using SAPbouiCOM.Framework;
using System;
using System.Threading.Tasks;

namespace ConnectIPS.Integration.Forms.SAP
{
    [FormAttribute("133", "Forms/SAP/A_R Invoice.b1f")]
    class A_R_Invoice : SystemFormBase
    {
        public A_R_Invoice()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.bQR = ((SAPbouiCOM.Button)(this.GetItem("bQRC").Specific));
            this.bQR.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.bQR_ClickBefore);
            this.bQR.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bQR_ClickAfter);
            this.bAdd = ((SAPbouiCOM.ButtonCombo)(this.GetItem("1").Specific));
            this.bAdd.ClickBefore += new SAPbouiCOM._IButtonComboEvents_ClickBeforeEventHandler(this.bAdd_ClickBefore);
            this.chPaid = ((SAPbouiCOM.CheckBox)(this.GetItem("chPay").Specific));
            this.tTotalAmt = ((SAPbouiCOM.EditText)(this.GetItem("29").Specific));
            this.tDN = ((SAPbouiCOM.EditText)(this.GetItem("8").Specific));
            this.tTraceID = ((SAPbouiCOM.EditText)(this.GetItem("tTrace").Specific));
            this.tCardCode = ((SAPbouiCOM.EditText)(this.GetItem("4").Specific));
            this.tDocDate = ((SAPbouiCOM.EditText)(this.GetItem("10").Specific));
            this.tDueDate = ((SAPbouiCOM.EditText)(this.GetItem("12").Specific));
            this.cbCurr = ((SAPbouiCOM.ComboBox)(this.GetItem("70").Specific));
            this.bPay = ((SAPbouiCOM.Button)(this.GetItem("bPaymt").Specific));
            this.bPay.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bPay_ClickAfter);
            this.cbBranch = ((SAPbouiCOM.ComboBox)(this.GetItem("2001").Specific));
            this.chQR = ((SAPbouiCOM.CheckBox)(this.GetItem("chQR").Specific));
            this.tQRAmt = ((SAPbouiCOM.EditText)(this.GetItem("tQrAmt").Specific));
            this.tProject = ((SAPbouiCOM.EditText)(this.GetItem("157").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.TxAppliedAmount = ((SAPbouiCOM.EditText)(this.GetItem("31").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("38").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.TxCASH = ((SAPbouiCOM.EditText)(this.GetItem("TxCH").Specific));
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
        }


        private SAPbouiCOM.Button bQR;
        private SAPbouiCOM.CheckBox chPaid;
        private SAPbouiCOM.EditText tTotalAmt;
        private SAPbouiCOM.EditText tDN;
        private SAPbouiCOM.EditText tTraceID;
        private SAPbouiCOM.ButtonCombo bAdd;
        private SAPbouiCOM.EditText tCardCode;
        private SAPbouiCOM.EditText tDocDate;
        private SAPbouiCOM.EditText tDueDate;
        private SAPbouiCOM.ComboBox cbCurr;
        private SAPbouiCOM.Button bPay;
        private SAPbouiCOM.ComboBox cbBranch;
        private SAPbouiCOM.CheckBox chQR;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.EditText tQRAmt;
        private SAPbouiCOM.EditText tProject;
        private SAPbouiCOM.EditText TxAppliedAmount;
        private SAPbouiCOM.Matrix Matrix0;

        private void bQR_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!bQrCodeValidation())
                BubbleEvent = false;
        }

        private void bQR_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                chQR.Checked = true;

                GenerateQRCode().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
            }
        }

        private void bPay_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            chPaid.Checked = true;
            bAdd.Item.Click();
        }

        private void bAdd_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!bAddValidation())
                BubbleEvent = false;
        }

        private bool bQrCodeValidation()
        {
            var strtotalAmount = tTotalAmt.Value.Split(' ')[1];
            var QRAmt = Convert.ToDouble(tQRAmt.Value);
            double.TryParse(strtotalAmount, out double amount);
            if (UIAPIRawForm.Mode != SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                Program.SBO_Application.StatusBar.SetText("Form is already Added.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tCardCode.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select the Customer.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (Matrix0.VisualRowCount == 1)
            {
                Program.SBO_Application.StatusBar.SetText("Please add the items.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (QRAmt == 0)
            {
                Program.SBO_Application.StatusBar.SetText("QR amount is zero.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task GenerateQRCode()
        {
            var strAppliedAmount = TxAppliedAmount.Value.Split(' ')[1];
            double.TryParse(strAppliedAmount, out double appliedAmount);
            var data = new QRGeneration()
            {
                transactionAmount = strAppliedAmount,
                billNumber = tDN.Value
            };

            var service = new DynamicQRService();
            var response = await service.GenerateQRAsync(data);
            if (response.responseCode != "000")
            {
                Program.SBO_Application.StatusBar.SetText(response.responseMessage, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return;
            }
            else
            {
                var traceID = response.data.validationTraceId;
                tTraceID.Value = traceID;
                string qrString = response.data.qrString;
                var form = new QRCodeScan(qrString, traceID, UIAPIRawForm.Type, UIAPIRawForm.TypeCount);
                form.Show();
            }
        }

        private bool bAddValidation()
        {
            if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                Program.SBO_Application.StatusBar.SetText("Nepal Pay QR Payment is not completed.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (!chPaid.Checked)
            {
                Program.SBO_Application.StatusBar.SetText("Please proceed QR payment.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText TxCASH;

        //private void QRConfimation()
        //{
        //    var amountString = tTotalAmt.Value.Split(' ')[1];
        //    double.TryParse(amountString, out double amount);

        //    QRAmountConfirmation confimation = new QRAmountConfirmation(amount.ToString(), UIAPIRawForm.Type, UIAPIRawForm.TypeCount);
        //    confimation.Show();
        //}

        //private void Form_DataAddAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        //{
        //    if (pVal.ActionSuccess && chQR.Checked && chPaid.Checked)
        //    {
        //        var xmlString = pVal.ObjectKey;
        //        // Load the XML string into an XDocument
        //        XDocument xdoc = XDocument.Parse(xmlString);

        //        // Get the value of the "DocEntry" element
        //        string strdocEntry = xdoc.Root.Element("DocEntry").Value;
        //        int.TryParse(strdocEntry, out int docEntry);

        //        var data = PrepareIncomingPaymentData(docEntry);
        //        IncomingPaymentService service = new IncomingPaymentService();
        //        service.Add(data);
        //    }
        //}

        //private void ShowQR()
        //{
        //    if (chQR.Checked && (!chFlag.Checked || !chPaid.Checked))
        //    {
        //        if (!string.IsNullOrEmpty(tTraceID.Value))
        //        {
        //            var input = Program.SBO_Application.MessageBox("Do you want to use previous QR code?", 1, "Yes", "No");
        //            if (input == 1)
        //            {
        //                //Old QR Code
        //                var form = new QRCodeScan(tTraceID.Value, UIAPIRawForm.Type, UIAPIRawForm.TypeCount);
        //                form.Show();
        //            }
        //            else
        //                QRConfimation();
        //        }
        //        else
        //            QRConfimation();
        //    }
        //}

        //private IncomingPayment PrepareIncomingPaymentData(int docEntry)
        //{
        //    //var amountString = tTotalAmt.Value.Split(' ')[1];
        //    //double.TryParse(amountString, out double amount);
        //    var amount = Convert.ToDouble(tQRAmt.Value);
        //    return new IncomingPayment()
        //    {
        //        CardCode = tCardCode.Value,
        //        DocDate = DateTime.ParseExact(tDocDate.Value, "yyyyMMdd", null),
        //        DueDate = DateTime.ParseExact(tDocDate.Value, "yyyyMMdd", null),
        //        //DocCurrency = cbCurr.Value,
        //        Branch = Convert.ToInt32(cbBranch.Value),
        //        PaymentAmount = amount,
        //        Project = tProject.Value,
        //        invoice = new Invoice()
        //        {
        //            DocEntry = docEntry
        //        }
        //    };
        //}

    }
}
