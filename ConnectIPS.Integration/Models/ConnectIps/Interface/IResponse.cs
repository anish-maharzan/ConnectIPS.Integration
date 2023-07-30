namespace ConnectIPS.Integration.Models.ConnectIps.Interface
{
    interface IResponse
    {
        string responseCode { get; set; }
        //string responseStatus { get; set; }
        string responseMessage { get; set; }
    }
}
