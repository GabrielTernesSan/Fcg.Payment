using Fcg.Payment.Common.Responses;
using Fcg.Payment.Proxy.User.Client.Interfaces;
using Fcg.Payment.Proxy.User.Client.Responses;
using Fcg.Payment.Proxy.User.Configurations;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Fcg.Payment.Proxy.User
{
    public class ClientUser : IClientUser
    {
        private readonly UserConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ClientUser(IOptions<UserConfiguration> configuration, HttpClient httpClient)
        {
            _configuration = configuration.Value;
            _httpClient = httpClient;
        }

        public async Task<bool> AddBalanceAsync(Guid userId, decimal amount)
        {
            var url = $"{_configuration.Url}/users/{userId}/credit";

            var payload = new
            {
                credit = amount
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<Response<GetUserResponse>> GetUserAsync(Guid userId)
        {
            var response = new Response<GetUserResponse>();
            var url = $"{_configuration.Url}/users/{userId}";

            try
            {
                var httpResponse = await _httpClient.GetAsync(url);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.AddError("Usuário não encontrado no sistema Fcg.User.");
                    return response;
                }

                var json = await httpResponse.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("result", out JsonElement resultElement))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    response.Result = JsonSerializer.Deserialize<GetUserResponse>(resultElement.GetRawText(), options);
                }
                else
                {
                    response.AddError("Estrutura do JSON inválida: campo 'result' não encontrado.");
                }
            }
            catch (Exception ex)
            {
                response.AddError($"Falha na comunicação com User API: {ex.Message}");
            }

            return response;
        }

        public async Task<bool> SubtractBalanceAsync(Guid userId, decimal amount)
        {
            var url = $"{_configuration.Url}/users/{userId}/debit";

            var payload = new
            {
                debit = amount
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }
    }
}
