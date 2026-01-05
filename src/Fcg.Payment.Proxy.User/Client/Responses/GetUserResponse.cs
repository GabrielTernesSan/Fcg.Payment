using System.Text.Json.Serialization;

namespace Fcg.Payment.Proxy.User.Client.Responses
{
    public class GetUserResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = null!;
        [JsonPropertyName("wallet")]
        public decimal Wallet { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
