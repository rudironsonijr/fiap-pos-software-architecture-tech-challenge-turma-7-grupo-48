using Domain.Entities.OrderAggregate;
using Domain.Entities.PaymentAggregate;
using Domain.Repositories;
using Infrastructure.Extensions.PaymentAggregate;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.PaymentAggregate.Extensions;

namespace Infrastructure.Adapters;

public class PaymentRepositoryAdpater : IPaymentRepository
{
	private readonly IPaymentSqlModelRepository _paymentSqlRepository;
	public PaymentRepositoryAdpater(IPaymentSqlModelRepository paymentSqlModelRepository)
	{
		_paymentSqlRepository = paymentSqlModelRepository;
	}
	public async Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken)
	{
		var paymentSql = payment.ToPaymentSqlModel();
		_paymentSqlRepository.Add(paymentSql);
		await _paymentSqlRepository.UnitOfWork.CommitAsync(cancellationToken);

		return
			paymentSql.ToPayment();
	}

	public async Task<Payment?> GetAsync(int id, CancellationToken cancellationToken)
	{
		var paymentSql = await _paymentSqlRepository.GetAsync(x => x.Id == id, true, cancellationToken);
		return
			paymentSql?.ToPayment();
	}

	public async Task<Payment?> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken)
	{
		var paymentSql = await _paymentSqlRepository.GetAsync(x => x.ExternalId == externalId, true, cancellationToken);
		return
			paymentSql?.ToPayment();
	}

	public async Task UpdateAsync(Payment payment, CancellationToken cancellationToken)
	{
		var paymentSql = await _paymentSqlRepository.GetAsync(x => x.Id == payment.Id, true, cancellationToken);

		if(paymentSql == null) {
			throw new ArgumentException($"Payment with id: {payment.Id} Not exists");
		}

		paymentSql.Status = payment.Status;
		paymentSql.PaymentKind = payment.PaymentMethod.Kind;
		paymentSql.PaymentProvider = payment.PaymentMethod.Provider;
		paymentSql.OrderId = payment.OrderId;
		paymentSql.ExternalId = payment.ExternalId;
		paymentSql.Amount = payment.Amount;

		_paymentSqlRepository.Update(paymentSql);
		await _paymentSqlRepository.UnitOfWork.CommitAsync(cancellationToken);
	}
}
