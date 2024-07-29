using Domain.Entities.PaymentAggregate;
using Domain.Repositories.Base;

namespace Domain.Repositories;

public interface IPaymentRepository : IRepository<Payment>
{
	Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken);
	Task<Payment> GetAsync(string externalId, CancellationToken cancellationToken);
	Task<Payment> UpdateAsync(Payment payment, CancellationToken cancellationToken);
}
