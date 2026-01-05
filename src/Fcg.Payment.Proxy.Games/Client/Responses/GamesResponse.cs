namespace Fcg.Payment.Proxy.Games.Client.Responses
{
    public class GamesResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Genre { get; set; }
    }
}
