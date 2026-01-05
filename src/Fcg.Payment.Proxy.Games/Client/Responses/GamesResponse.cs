using System.Text.Json.Serialization;

namespace Fcg.Payment.Proxy.Games.Client.Responses
{
    public class GamesResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("genre")]
        public int Genre { get; set; }

        [JsonPropertyName("promotion")]
        public object? Promotion { get; set; }
    }
}
