namespace Fcg.Payment.Application.Requests
{
    public class PurchaseGameRequest
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
    }
}
