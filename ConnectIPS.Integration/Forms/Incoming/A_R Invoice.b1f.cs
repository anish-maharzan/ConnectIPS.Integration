using ConnectIPS.Integration.Forms.Incoming;
using ConnectIPS.Integration.Models.ConnectIps;
using ConnectIPS.Integration.Models.ConnectIps.Interface;
using ConnectIPS.Integration.Models.ConnectIps.Response;
using ConnectIPS.Integration.Models.SAP;
using ConnectIPS.Integration.Services.ConnectIps;
using ConnectIPS.Integration.Services.SAP;
using MainLibrary.SAPB1;
using SAPbouiCOM.Framework;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConnectIPS.Integration
{
    [FormAttribute("133", "Forms/Incoming/A_R Invoice.b1f")]
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
            this.chFlag = ((SAPbouiCOM.CheckBox)(this.GetItem("ChFlag").Specific));
            this.chQR = ((SAPbouiCOM.CheckBox)(this.GetItem("chQR").Specific));
            this.tQRAmt = ((SAPbouiCOM.EditText)(this.GetItem("tQrAmt").Specific));
            this.OnCustomInitialize();
        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataAddAfter += new DataAddAfterHandler(this.Form_DataAddAfter);

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
        private SAPbouiCOM.CheckBox chFlag;
        private SAPbouiCOM.CheckBox chQR;
        private SAPbouiCOM.EditText tQRAmt;

        private void OnCustomInitialize()
        {

        }

        private bool bQrCodeValidation()
        {
            var amountString = tTotalAmt.Value.Split(' ')[1];
            double.TryParse(amountString, out double amount);
            if (UIAPIRawForm.Mode != SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                Program.SBO_Application.StatusBar.SetText("Form is not in Add Mode.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tCardCode.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Please select Card Code.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (amount == 0)
            {
                Program.SBO_Application.StatusBar.SetText("Total Amount is zero.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void bQR_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!bQrCodeValidation())
                BubbleEvent = false;
        }

        private async Task<QRGenerationResponse> GenerateQRCode()
        {
            var data = GetQrCodeData();
            var service = new ConnectIpsService();
            return await service.GenerateQRAsync(data);
        }

        private QRGeneration GetQrCodeData()
        {
            //var amountString = tTotalAmt.Value.Split(' ')[1];
            //double.TryParse(amountString, out double amount);

            var qrAmt = Convert.ToDouble(tQRAmt.Value);
            return new QRGeneration()
            {
                transactionAmount = qrAmt.ToString(),
                billNumber = tDN.Value
            };
        }

        private void QRConfimation()
        {
            var amountString = tTotalAmt.Value.Split(' ')[1];
            double.TryParse(amountString, out double amount);

            QRAmountConfirmation confimation = new QRAmountConfirmation(amount.ToString(), UIAPIRawForm.Type, UIAPIRawForm.TypeCount);
            confimation.Show();
        }

        private void bQR_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var response = GenerateQRCode().GetAwaiter().GetResult();

            if (response.responseCode != "000")
            {
                Program.SBO_Application.StatusBar.SetText(response.responseMessage, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return;
            }
            else
            {
                tTraceID.Value = response.data.validationTraceId;
                string qrString = response.data.qrString;
                var form = new QRCodeScan(qrString, response.data.validationTraceId, UIAPIRawForm.Type, UIAPIRawForm.TypeCount);
                form.Show();
                Program.SBO_Application.StatusBar.SetText("QR Code generated successfully.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
        }

        private void bPay_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            chPaid.Checked = true;
        }

        private bool bAddValidation()
        {
            if (!chPaid.Checked)
            {
                Program.SBO_Application.StatusBar.SetText("Please proceed QR payment.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private void bAdd_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                //if (!bAddValidation())
                //{
                //    BubbleEvent = false;
                //    return;
                //}
                if (chQR.Checked && (!chFlag.Checked || !chPaid.Checked))
                {
                    BubbleEvent = false;
                    if (!string.IsNullOrEmpty(tTraceID.Value))
                    {
                        var input = Program.SBO_Application.MessageBox("Do you want to use previous QR code?", 1, "Yes", "No");
                        if (input == 1)
                        {
                            //Old QR Code
                            var form = new QRCodeScan(tTraceID.Value, UIAPIRawForm.Type, UIAPIRawForm.TypeCount);
                            form.Show();
                        }
                        else
                            QRConfimation();
                    }
                    else
                        QRConfimation();
                }
                //else
                //{
                //    Program.SBO_Application.StatusBar.SetText("Please proceed QR payment.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                //    BubbleEvent = false;
                //}
            }
        }

        private IncomingPayment PrepareIncomingPaymentData(int docEntry)
        {
            var amountString = tTotalAmt.Value.Split(' ')[1];
            double.TryParse(amountString, out double amount);

            return new IncomingPayment()
            {
                CardCode = tCardCode.Value,
                DocDate = DateTime.ParseExact(tDocDate.Value, "yyyyMMdd", null),
                DueDate = DateTime.ParseExact(tDocDate.Value, "yyyyMMdd", null),
                //DocCurrency = cbCurr.Value,
                Branch = Convert.ToInt32(cbBranch.Value),
                PaymentAmount = amount,
                invoice = new Invoice()
                {
                    DocEntry = docEntry
                }
            };
        }

        private void Form_DataAddAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            //if (pVal.ActionSuccess)
            //{
            //    var xmlString = pVal.ObjectKey;
            //    // Load the XML string into an XDocument
            //    XDocument xdoc = XDocument.Parse(xmlString);

            //    // Get the value of the "DocEntry" element
            //    string strdocEntry = xdoc.Root.Element("DocEntry").Value;
            //    int.TryParse(strdocEntry, out int docEntry);

            //    IncomingPaymentService service = new IncomingPaymentService();
            //    service.Add(PrepareIncomingPaymentData(docEntry));
            //}
        }
    }
}
