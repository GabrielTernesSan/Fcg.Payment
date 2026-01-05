using Fcg.Payment.Common.Responses;
using Fcg.Payment.Proxy.Games.Client.Interfaces;
using Fcg.Payment.Proxy.Games.Client.Responses;
using Fcg.Payment.Proxy.Games.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Fcg.Payment.Proxy.Games
{
    public class ClientGames : IClientGames
    {
        private readonly GamesConfiguration _gamesConfiguration;
        private readonly HttpClient _httpClient;

        public ClientGames(IOptions<GamesConfiguration> gamesConfiguration, HttpClient httpClient)
        {
            _gamesConfiguration = gamesConfiguration.Value;
            _httpClient = httpClient;
        }

        public async Task<Response<GamesResponse>> GetGameAsync(Guid id)
        {
            var response = new Response<GamesResponse>();

            var url = $"{_gamesConfiguration.Url}/api/games/{id}";

            try
            {
                using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    var status = (int)httpResponseMessage.StatusCode;
                    var reason = httpResponseMessage.ReasonPhrase ?? "Sem motivo informado";

                    response.AddError($"Erro ao enviar dados ({status} {reason}).");
                    return response;
                }

                var json = await httpResponseMessage.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(json))
                    response.Result = JsonSerializer.Deserialize<GamesResponse>(json);
            }
            catch (Exception ex)
            {
                response.AddError($"Erro ao receber dados ({ex.Message})");
            }

            return response;
        }
    }
}
