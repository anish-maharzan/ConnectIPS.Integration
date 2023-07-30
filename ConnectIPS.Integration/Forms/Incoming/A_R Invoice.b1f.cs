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
            this.chPay = ((SAPbouiCOM.CheckBox)(this.GetItem("chPay").Specific));
            this.tTotalAmt = ((SAPbouiCOM.EditText)(this.GetItem("29").Specific));
            this.tDN = ((SAPbouiCOM.EditText)(this.GetItem("8").Specific));
            this.tTraceID = ((SAPbouiCOM.EditText)(this.GetItem("tTrace").Specific));
            this.tCardCode = ((SAPbouiCOM.EditText)(this.GetItem("4").Specific));
            this.tDocDate = ((SAPbouiCOM.EditText)(this.GetItem("10").Specific));
            this.tDueDate = ((SAPbouiCOM.EditText)(this.GetItem("12").Specific));
            this.cbCurr = ((SAPbouiCOM.ComboBox)(this.GetItem("70").Specific));
            this.bPay = ((SAPbouiCOM.Button)(this.GetItem("bPaymt").Specific));
            this.bPay.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bPay_ClickAfter);
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
        private SAPbouiCOM.CheckBox chPay;
        private SAPbouiCOM.EditText tTotalAmt;
        private SAPbouiCOM.EditText tDN;
        private SAPbouiCOM.EditText tTraceID;
        private SAPbouiCOM.ButtonCombo bAdd;
        private SAPbouiCOM.EditText tCardCode;
        private SAPbouiCOM.EditText tDocDate;
        private SAPbouiCOM.EditText tDueDate;
        private SAPbouiCOM.ComboBox cbCurr;
        private SAPbouiCOM.Button bPay;

        private void OnCustomInitialize()
        {

        }

        private void bQR_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!bQrCodeValidation())
                BubbleEvent = false;
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

        private void bQR_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var response = GetQRCode().GetAwaiter().GetResult();

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

        private async Task<QRGenerationResponse> GetQRCode()
        {
            var service = new ConnectIpsService();
            return await service.GenerateQRAsync(PrepareQrPaymentData());
        }

        private QRGeneration PrepareQrPaymentData()
        {
            var amountString = tTotalAmt.Value.Split(' ')[1];
            double.TryParse(amountString, out double amount);
            return new QRGeneration()
            {
                transactionAmount = amount.ToString(),
                billNumber = tDN.Value
            };
        }

        private void bAdd_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!bAddValidation())
            {
                BubbleEvent = false;
                return;
            }
        }

        private bool bAddValidation()
        {
            if (!chPay.Checked)
            {
                VerifyPayment();
                Program.SBO_Application.StatusBar.SetText("Please process QR payment.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private void VerifyPayment()
        {
            try
            {
                var response = PaymentVerify().GetAwaiter().GetResult();
                if (response.responseCode == "200")
                {
                    var respnse = (PaymentVerificationSuccessResponse)response;
                    chPay.Checked = true;
                }
                else
                {
                    var respnse = (PaymentVerificationErrorResponse)response;
                    Program.SBO_Application.MessageBox(respnse.responseDescription);
                }
            }
            catch (System.Exception ex)
            {
                Program.SBO_Application.MessageBox($"Exception occurs: {ex.Message}");
            }
        }

        private async Task<IResponse> PaymentVerify()
        {
            var paymentVerification = new PaymentVerification()
            {
                acquirerId = Credential.AcquirerId,
                merchantId = Credential.MerchantCode,
                validationTraceId = tTraceID.Value
            };
            var service = new ConnectIpsService();
            var response = await service.VerifyPayment(paymentVerification);
            return response;
        }

        private IncomingPayment PrepareIncomingPaymentData()
        {
            var amountString = tTotalAmt.Value.Split(' ')[1];
            double.TryParse(amountString, out double amount);
            var str = B1Helper.DiCompany.GetNewObjectKey();
            int docEntry = Convert.ToInt32(str);

            return new IncomingPayment()
            {
                CardCode = tCardCode.Value,
                DocDate = DateTime.ParseExact(tDocDate.Value, "yyyyMMdd", null),
                DueDate = DateTime.ParseExact(tDocDate.Value, "yyyyMMdd", null),
                DocCurrency = cbCurr.Value,
                PaymentAmount = amount,
                invoice = new Invoice()
                {
                    DocEntry = docEntry
                }
            };
        }

        private void bPay_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            chPay.Checked = true;
        }

        private void Form_DataAddAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            if (pVal.ActionSuccess)
            {
                IncomingPaymentService service = new IncomingPaymentService();
                service.Add(PrepareIncomingPaymentData());
            }
        }
    }
}
