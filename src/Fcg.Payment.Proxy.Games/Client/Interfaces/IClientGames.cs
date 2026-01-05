using Fcg.Payment.Common.Responses;
using Fcg.Payment.Proxy.Games.Client.Responses;

namespace Fcg.Payment.Proxy.Games.Client.Interfaces
{
    public interface IClientGames
    {
        Task<Response<GamesResponse>> GetGameAsync(Guid id);
    }
}
