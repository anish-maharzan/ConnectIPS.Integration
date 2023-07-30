using ConnectIPS.Integration.Models.ConnectIps;
using ConnectIPS.Integration.Models.ConnectIps.Interface;
using ConnectIPS.Integration.Models.ConnectIps.Response;
using ConnectIPS.Integration.Services.ConnectIps;
using QRCoder;
using SAPbouiCOM.Framework;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ConnectIPS.Integration.Forms.Incoming
{
    [FormAttribute("ConnectIPS.Integration.Forms.Incoming.QRCode", "Forms/Incoming/QRCodeScan.b1f")]
    class QRCodeScan : UserFormBase
    {
        private SAPbouiCOM.Button bCheck;
        private SAPbouiCOM.PictureBox pbQrCode;
        private readonly string _validationTraceId;
        private readonly string _qrString;
        private int _type;
        private int _count;

        public QRCodeScan()
        {
        }

        public QRCodeScan(string qrString, string validationTraceId, int type, int count)
        {
            _type = type;
            _count = count;
            _qrString = qrString;
            _validationTraceId = validationTraceId;
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
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private void DisplayQRCode()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(_qrString, QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10); // 10 is the size (pixels) of each QR code module (cell)

            var qrCodeImgName = _validationTraceId + "_QrCodeImage.png";

            var qrCodePath = System.Windows.Forms.Application.StartupPath + "\\QRCode";
            if (!Directory.Exists(qrCodePath))
                Directory.CreateDirectory(qrCodePath);

            qrCodeImage.Save("QRCode\\" + qrCodeImgName, System.Drawing.Imaging.ImageFormat.Png);
            pbQrCode.Picture = Path.Combine(qrCodePath, qrCodeImgName);
        }

        private void OnCustomInitialize()
        {
        }

        private void bCheck_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                var response = PaymentVerify().GetAwaiter().GetResult();
                if (response.responseCode == "200")
                {
                    var respnse = (PaymentVerificationSuccessResponse)response;
                    Program.SBO_Application.MessageBox(respnse.responseStatus);
                    var form = (SAPbouiCOM.Form)Application.SBO_Application.Forms.GetFormByTypeAndCount(_type, _count);
                    var paymentButton = (SAPbouiCOM.Button)form.Items.Item("bPaymt").Specific;
                    paymentButton.Item.Click();
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
                validationTraceId = _validationTraceId
            };
            var service = new ConnectIpsService();
            var response = await service.VerifyPayment(paymentVerification);
            return response;
        }
    }
}
