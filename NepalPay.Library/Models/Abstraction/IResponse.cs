namespace NepalPay.Library.Models.Abstraction
{
    public interface IResponse
    {
        string responseCode { get; set; }
        //string responseStatus { get; set; }
        string responseMessage { get; set; }
    }
}
