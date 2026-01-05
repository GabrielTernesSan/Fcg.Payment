using Fcg.Payment.Domain.Enums;

namespace Fcg.Payment.Domain.Entities
{
    public class PaymentTransaction
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? GameId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        public PaymentTransaction(Guid userId, decimal amount, Guid? gameId = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Amount = amount;
            Status = PaymentStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            GameId = gameId;
        }

        public void Approve()
        {
            Status = PaymentStatus.Approved;
            CompletedAt = DateTime.UtcNow;
        }

        public void Reject()
        {
            Status = PaymentStatus.Rejected;
            CompletedAt = DateTime.UtcNow;
        }
    }
}