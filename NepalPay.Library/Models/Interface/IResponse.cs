namespace NepalPay.Library.Models.Interface
{
    public interface IResponse
    {
        string responseCode { get; set; }
        //string responseStatus { get; set; }
        string responseMessage { get; set; }
    }
}
