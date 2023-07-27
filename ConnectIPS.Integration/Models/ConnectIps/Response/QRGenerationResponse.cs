namespace ConnectIPS.Integration.Models.ConnectIps.Response
{
    public class QRGenerationResponse
    {
        public string timestamp { get; set; }
        public string responseCode { get; set; }
        public string responseStatus { get; set; }
        public string responseMessage { get; set; }
        public QrDetails data { get; set; }
        public QRGenerationResponse()
        {
            data = new QrDetails();
        }
    }

    public class QrDetails
    {
        public string qrString { get; set; }
        public string validationTraceId { get; set; }
    }
}
