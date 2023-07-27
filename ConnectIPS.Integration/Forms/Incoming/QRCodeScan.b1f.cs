using ConnectIPS.Integration.Models.ConnectIps;
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
        private SAPbouiCOM.Button bScan;
        private SAPbouiCOM.PictureBox pbQrCode;
        private readonly string _validationTraceId;
        private readonly string _qrString;

        public QRCodeScan()
        {
        }

        public QRCodeScan(string qrString, string validationTraceId)
        {
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
            this.bScan = ((SAPbouiCOM.Button)(this.GetItem("bScan").Specific));
            this.bScan.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.bScan_ClickAfter);
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
            pbQrCode.Picture = Path.Combine( qrCodePath , qrCodeImgName);
        }

        private void OnCustomInitialize()
        {

        }

        private void bScan_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                var response = PaymentVerify().GetAwaiter().GetResult();
                if (response.responseCode == "000")
                {
                    Program.SBO_Application.MessageBox(response.responseMessage);
                }
                else
                {
                    Program.SBO_Application.MessageBox("Not Recieved yet.");
                }
                Program.SBO_Application.MessageBox(response.responseMessage);
            }
            catch (System.Exception ex)
            {
                Program.SBO_Application.MessageBox(ex.Message);
            }
        }

        private async Task<PaymentVerificationResponse> PaymentVerify()
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
