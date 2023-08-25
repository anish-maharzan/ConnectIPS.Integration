using NepalPay.Library.Data;
using NepalPay.Library.Models;
using NepalPay.Library.Models.Interface;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Services;
using QRCoder;
using SAPbouiCOM.Framework;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ConnectIPS.Integration.Forms.Incoming
{
    [FormAttribute("ConnectIPS.Integration.Forms.Incoming.QRCode", "Forms/Incoming/QRCodeScan.b1f")]
    class QRCodeScan : UserFormBase
    {
        private readonly string _validationTraceId;
        private readonly string _qrString;
        private readonly int _type;
        private readonly int _count;

        private SAPbouiCOM.Button bCheck;
        private SAPbouiCOM.PictureBox pbQrCode;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.CheckBox ChPaid;

        public QRCodeScan()
        {
        }

        public QRCodeScan(string validationTraceId, int type, int count)
        {
            _type = type;
            _count = count;
            _validationTraceId = validationTraceId;
            DisplayQRCode();
        }

        public QRCodeScan(string qrString, string validationTraceId, int type, int count)
        {
            _type = type;
            _count = count;
            _validationTraceId = validationTraceId;
            _qrString = qrString;
            DisplayQRCode();
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.pbQrCode = ((SAPbouiCOM.PictureBox)(this.GetItem("pQr").Specific));
            this.bCheck = ((SAPbouiCOM.Button)(this.GetItem("bChk").Specific));
            this.bCheck.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bCheck_ClickAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.ChPaid = ((SAPbouiCOM.CheckBox)(this.GetItem("ChPaid").Specific));
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
            UIAPIRawForm.Left = (Program.SBO_Application.Desktop.Width - UIAPIRawForm.Width)/2;
            UIAPIRawForm.Top = Convert.ToInt32( (Program.SBO_Application.Desktop.Height - UIAPIRawForm.Height)/2.5);
        }

        private void DisplayQRCode()
        {
            string qrCodePath = GetQrCodeImg();
            pbQrCode.Picture = qrCodePath;
        }

        private string GetQrCodeImg()
        {
            var folder = Path.Combine(System.Windows.Forms.Application.StartupPath, "QRCode");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var imgName = _validationTraceId + ".png";

            var fullPath = Path.Combine(folder, imgName);

            if (File.Exists(fullPath))
                return fullPath;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(_qrString, QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10); // 10 is the size (pixels) of each QR code module (cell)

            qrCodeImage.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);

            return fullPath;
        }

        private void bCheck_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CheckPaymentStatus();
        }

        private void CheckPaymentStatus()
        {
            try
            {
                var response = PaymentVerify().GetAwaiter().GetResult();
                if (response.responseCode == "200")
                {
                    var form = Application.SBO_Application.Forms.GetFormByTypeAndCount(_type, _count);
                    var paymentButton = (SAPbouiCOM.Button)form.Items.Item("bPaymt").Specific;
                    paymentButton.Item.Click();
                    var respnse = (PaymentVerificationSuccessResponse)response;
                    //Program.SBO_Application.StatusBar.SetText(respnse.responseStatus, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    Program.SBO_Application.StatusBar.SetText("Payment through NEPALPAY QR is done successfully.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    ChPaid.Checked = true;
                    Button0.Item.Click();
                }
                else
                {
                    var respnse = (PaymentVerificationErrorResponse)response;
                    Program.SBO_Application.StatusBar.SetText(respnse.responseDescription, SAPbouiCOM.BoMessageTime.bmt_Medium,SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.MessageBox($"Error occurs: {ex.Message}");
            }
        }

        private async Task<IResponse> PaymentVerify()
        {
            var paymentVerification = new PaymentVerification()
            {
                acquirerId = Credential.AcquirerId,
                merchantId = Credential.MerchantCode,
                validationTraceId = _validationTraceId
            };
            var service = new NepalPayService();
            var response = await service.VerifyPayment(paymentVerification);
            return response;
        }
    }
}
