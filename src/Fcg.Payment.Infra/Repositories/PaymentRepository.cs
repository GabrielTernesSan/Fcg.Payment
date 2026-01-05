using Fcg.Payment.Domain.Entities;
using Fcg.Payment.Domain.Repositories;
using Fcg.Payment.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Fcg.Payment.Infra.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Payments.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task AddAsync(PaymentTransaction payment, CancellationToken cancellationToken = default)
        {
            await _context.Payments.AddAsync(payment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(PaymentTransaction payment, CancellationToken cancellationToken = default)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}