using Fcg.Payment.Common.Responses;
using Fcg.Payment.Proxy.Auth.Client.Responses;

namespace Fcg.Payment.Proxy.Auth.Client.Interfaces
{
    public interface IClientAuth
    {
        Task<Response<GetUserAuthResponse>> GetAuthUserAsync(Guid id);
    }
}
