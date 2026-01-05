using Fcg.Payment.Common.Responses;
using Fcg.Payment.Proxy.Auth.Client.Interfaces;
using Fcg.Payment.Proxy.Auth.Client.Responses;
using Fcg.Payment.Proxy.Auth.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Fcg.Payment.Proxy.Auth.Client
{
    public class ClientAuth : IClientAuth
    {
        private readonly AuthConfiguration _authConfiguration;
        private readonly HttpClient _httpClient;

        public ClientAuth(IOptions<AuthConfiguration> authConfiguration, HttpClient httpClient)
        {
            _authConfiguration = authConfiguration.Value;
            _httpClient = httpClient;
        }

        public async Task<Response<GetUserAuthResponse>> GetAuthUserAsync(Guid id)
        {
            var response = new Response<GetUserAuthResponse?>();

            var url = $"{_authConfiguration.Url}/auth/users/{id}/email";

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
                    response.Result = JsonSerializer.Deserialize<GetUserAuthResponse>(json);
            }
            catch (Exception ex)
            {
                response.AddError($"Erro ao enviar dados ({ex.Message})");
            }

            return response;
        }
    }
}
