namespace Fcg.Payment.Proxy.User.Client.Responses
{
    public class GetUserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public decimal Wallet { get; set; }
        public string? Email { get; set; }
    }
}
