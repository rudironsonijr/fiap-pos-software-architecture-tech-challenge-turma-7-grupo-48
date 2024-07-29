using Domain.Entities.OrderAggregate;
using Domain.Entities.PaymentAggregate;
using Domain.Repositories;

namespace Infrastructure.Adapters;

public class PaymentRepositoryAdpater : IPaymentRepository
{
	public async Task<Payment> CreateAsync(Payment order, CancellationToken cancellationToken)
	{

	}
}
