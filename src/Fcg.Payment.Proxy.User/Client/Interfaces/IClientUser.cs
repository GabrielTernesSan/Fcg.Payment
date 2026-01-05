using Fcg.Payment.Common.Responses;
using Fcg.Payment.Proxy.User.Client.Responses;

namespace Fcg.Payment.Proxy.User.Client.Interfaces
{
    public interface IClientUser
    {
        Task<bool> AddBalanceAsync(Guid userId, decimal amount);
        Task<bool> SubtractBalanceAsync(Guid userId, decimal amount);
        Task<Response<GetUserResponse>> GetUserAsync(Guid userId);
    }
}
