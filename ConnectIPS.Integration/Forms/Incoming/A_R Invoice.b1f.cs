using ConnectIPS.Integration.Forms.Incoming;
using ConnectIPS.Integration.Models.ConnectIps;
using ConnectIPS.Integration.Models.ConnectIps.Response;
using ConnectIPS.Integration.Services.ConnectIps;
using SAPbouiCOM.Framework;
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
            this.ButtonCombo0 = ((SAPbouiCOM.ButtonCombo)(this.GetItem("1").Specific));
            this.ButtonCombo0.ClickBefore += new SAPbouiCOM._IButtonComboEvents_ClickBeforeEventHandler(this.ButtonCombo0_ClickBefore);
            this.chPay = ((SAPbouiCOM.CheckBox)(this.GetItem("chPay").Specific));
            this.tTotalAmt = ((SAPbouiCOM.EditText)(this.GetItem("29").Specific));
            this.tDN = ((SAPbouiCOM.EditText)(this.GetItem("8").Specific));
            this.tTraceID = ((SAPbouiCOM.EditText)(this.GetItem("tTrace").Specific));
            this.OnCustomInitialize();
        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button bQR;
        private SAPbouiCOM.CheckBox chPay;
        private SAPbouiCOM.EditText tTotalAmt;
        private SAPbouiCOM.EditText tDN;
        private SAPbouiCOM.EditText tTraceID;
        private SAPbouiCOM.ButtonCombo ButtonCombo0;

        private void OnCustomInitialize()
        {

        }

        private void bQR_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!bQrValidation())
                BubbleEvent = false;
        }

        private bool bQrValidation()
        {
            if (UIAPIRawForm.Mode != SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                Program.SBO_Application.StatusBar.SetText("Try in Add Mode", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tTotalAmt.Value))
            {
                Program.SBO_Application.StatusBar.SetText("Amount is empty", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
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
                chPay.Checked = false;
                Program.SBO_Application.StatusBar.SetText(response.responseMessage, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return;
            }
            else
            {
                tTraceID.Value = response.data.validationTraceId;
                chPay.Checked = true;
                string qrString = response.data.qrString;
                var form = new QRCodeScan(qrString, response.data.validationTraceId);
                form.Show();
                Program.SBO_Application.StatusBar.SetText(response.responseMessage, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
        }

        private async Task<QRGenerationResponse> GetQRCode()
        {
            var service = new ConnectIpsService();
            return await service.GenerateQRAsync(PrepareData());
        }

        private QRGeneration PrepareData()
        {
            var amountString = tTotalAmt.Value.Split(' ')[1];
            double.TryParse(amountString, out double amount);
            return new QRGeneration()
            {
                transactionAmount = amount.ToString(),
                billNumber = tDN.Value
            };
        }

        private void ButtonCombo0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (!bAddValidation())
                BubbleEvent = false;
        }

        private bool bAddValidation()
        {
            if (!chPay.Checked)
            {
                Program.SBO_Application.StatusBar.SetText("Please pay first.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

    }
}
