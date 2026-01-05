using Fcg.Payment.Domain.Entities;

namespace Fcg.Payment.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task<PaymentTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(PaymentTransaction payment, CancellationToken cancellationToken = default);
        Task UpdateAsync(PaymentTransaction payment, CancellationToken cancellationToken = default);
    }
}