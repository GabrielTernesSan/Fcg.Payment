namespace Fcg.Payment.Application.Requests
{
    public class CheckoutRequest
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}