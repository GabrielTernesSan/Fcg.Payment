namespace Fcg.Payment.Application.Responses
{
    public class CheckoutResponse
    {
        public Guid? PaymentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}