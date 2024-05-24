using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;
using Infrastructure.Exceptions;
using Infrastructure.Extensions.OrderAggregate;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.SqlModels.OrderAggregate.Extensions;

namespace Infrastructure.Adapters;

public class OrderRepositoryAdpater : IOrderRepository
{
	private readonly IOrderSqlRepository _orderSqlRepository;
	public OrderRepositoryAdpater(IOrderSqlRepository orderSqlRepository)
	{
		_orderSqlRepository = orderSqlRepository;
	}

	public async Task<int> CreateAsync(Order order, CancellationToken cancellationToken)
	{
		var orderSql = order.ToOrderSqlModel();

		_orderSqlRepository.Add(orderSql);
		await _orderSqlRepository.UnitOfWork.CommitAsync(cancellationToken);

		return orderSql.Id;

	}

	public async Task<Order?> GetAsync(int id, CancellationToken cancellationToken)
	{
		var orderSql = await _orderSqlRepository.GetAsync(x => x.Id == id, true, cancellationToken);
		return
			orderSql?.ToOrder();
	}

	public async Task<IEnumerable<Order>> ListAsync(OrderStatus orderStatus, int? page, 
		int? limit, CancellationToken cancellationToken)
	{
		var orderSql = await _orderSqlRepository.ListAsync(x => x.Status == orderStatus, 
			page, limit, cancellationToken);

		var response = orderSql.Select(x => x.ToOrder());
		return response;

	}

	public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken)
	{
		var orderSql = await _orderSqlRepository.GetAsync(x => x.Id == order.Id, true, cancellationToken);

		NotFoundException.ThrowIfPropertyNull(orderSql, typeof(Order), "Id", order.Id); ;
		
		throw new NotImplementedException();
	}
}
