using Domain.Entities.PaymentAggregate;
using Domain.Repositories.Base;

namespace Domain.Repositories;

public interface IPaymentRepository : IRepository<Payment>
{
	Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken);
	Task<Payment?> GetAsync(int id, CancellationToken cancellationToken);
	Task<Payment?> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken);
	Task UpdateAsync(Payment payment, CancellationToken cancellationToken);
}
