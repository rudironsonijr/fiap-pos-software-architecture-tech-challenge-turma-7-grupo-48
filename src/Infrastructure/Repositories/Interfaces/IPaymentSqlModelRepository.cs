using Infrastructure.Repositories.Base;
using Infrastructure.SqlModels.PaymentAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces;

public interface IPaymentSqlModelRepository : ISqlRepository
{
	PaymentSqlModel Add(PaymentSqlModel payment);
	void Update(PaymentSqlModel payment);
	public Task<PaymentSqlModel?> GetAsync(Expression<Func<PaymentSqlModel, bool>> expression,
		bool asNoTracking, CancellationToken cancellationToken);
	Task<IEnumerable<PaymentSqlModel>> ListAsync(Expression<Func<PaymentSqlModel, bool>> expression,
		int? page, int? limit, CancellationToken cancellationToken);
}
