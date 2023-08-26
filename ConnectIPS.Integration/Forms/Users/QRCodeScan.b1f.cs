using NepalPay.Library.Credentials;
using NepalPay.Library.Models.Abstraction;
using NepalPay.Library.Models.Response;
using NepalPay.Library.Models.Transaction;
using NepalPay.Library.Services.Implementation;
using QRCoder;
using SAPbouiCOM.Framework;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ConnectIPS.Integration.Forms.Users
{
    [FormAttribute("ConnectIPS.Integration.Forms.Users.QRCodeScan", "Forms/Users/QRCodeScan.b1f")]
    class QRCodeScan : UserFormBase
    {
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
            this.BtnCancel = ((SAPbouiCOM.Button)(this.GetItem("BtnCancel").Specific));
            this.BtnCancel.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.BtnCancel_ClickAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
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

        private readonly string _validationTraceId;
        private readonly string _qrString;
        private readonly int _type;
        private readonly int _count;

        private SAPbouiCOM.Button bCheck;
        private SAPbouiCOM.PictureBox pbQrCode;
        private SAPbouiCOM.Button BtnCancel;
        private SAPbouiCOM.Button Button0;


        private void bCheck_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CheckPaymentStatus();
        }

        private void BtnCancel_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Button0.Item.Click();
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

        private async Task CheckPaymentStatus()
        {
            try
            {
                var response = await VerifyPayment();
                if (response.responseCode == "200")
                {
                    var arInvoice = Application.SBO_Application.Forms.GetFormByTypeAndCount(_type, _count);
                    var paymentButton = (SAPbouiCOM.Button)arInvoice.Items.Item("bPaymt").Specific;
                    paymentButton.Item.Click();

                    var respnse = (PaymentVerificationSuccessResponse)response;
                    Program.SBO_Application.StatusBar.SetText("NepalPay QR Payment is successful.", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    BtnCancel.Item.Click();
                }
                else
                {
                    var respnse = (PaymentVerificationErrorResponse)response;
                    Program.SBO_Application.StatusBar.SetText(respnse.responseDescription, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                }
            }
            catch (Exception ex)
            {
                Program.SBO_Application.MessageBox($"Error occurs: {ex.Message}");
            }
        }

        private async Task<IResponse> VerifyPayment()
        {
            var paymentVerification = new PaymentVerification()
            {
                acquirerId = NCHLCredential.AcquirerId,
                merchantId = NCHLCredential.MerchantCode,
                validationTraceId = _validationTraceId
            };
            var service = new DynamicQRService();
            var response = await service.VerifyPayment(paymentVerification);
            return response;
        }

    }
}
