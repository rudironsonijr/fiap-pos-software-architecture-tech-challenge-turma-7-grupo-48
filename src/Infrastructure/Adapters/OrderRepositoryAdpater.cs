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
	private readonly IOrderProductRepository _orderProductRepository;
	public OrderRepositoryAdpater(IOrderSqlRepository orderSqlRepository, IOrderProductRepository orderProductRepository)
	{
		_orderSqlRepository = orderSqlRepository;
		_orderProductRepository = orderProductRepository;
	}

	public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken)
	{
		var orderSql = order.ToOrderSqlModel();

		_orderSqlRepository.Add(orderSql);
		await _orderSqlRepository.UnitOfWork.CommitAsync(cancellationToken);
		return await GetAsync(orderSql.Id, cancellationToken) ?? throw new Exception("Order not Created");

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

	public async Task<IEnumerable<Order>> ListAsync(IEnumerable<OrderStatus> orderStatus, int? page,
		int? limit, CancellationToken cancellationToken)
	{
		var orderSql = await _orderSqlRepository.ListAsync(x => orderStatus.Contains(x.Status),
			page, limit, cancellationToken);

		var response = orderSql.Select(x => x.ToOrder());
		return response;

	}

	public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
	{
		var orderSql = await _orderSqlRepository.GetAsync(x => x.Id == order.Id, false, cancellationToken);

		EntityNotFoundException.ThrowIfPropertyNull(orderSql, typeof(Order), "Id", order.Id);

		orderSql!.Status = order.Status;
		orderSql.PaymentKind = order.PaymentMethod?.Kind;
		orderSql.PaymentProvider = order.PaymentMethod?.Provider;
		orderSql.Price = order.Price;

		var orderProductsId = order.OrderProducts.Select(x => x.Id);
		var toRemove = orderSql.OrderProducts.Where(x => !orderProductsId.Contains(x.Id)).ToList();
		if (toRemove.Count > 0)
		{
			_orderProductRepository.Remove(toRemove);
		}

		var orderProductUpdate = order.OrderProducts
			.Where(x => x.Id != 0)
			.Select(x => x.ToOrderProductSqlModel());

		foreach (var updateOrderProduct in orderProductUpdate)
		{
			var item = orderSql.OrderProducts.Where(x => x.Id == updateOrderProduct.Id).First();
			item.Quantity = updateOrderProduct.Quantity;
			item.ProductPrice = updateOrderProduct.ProductPrice;
			_orderProductRepository.Update(item);

		}

		var orderProductAdd = order.OrderProducts
			.Where(x => x.Id == 0)
			.Select(x => x.ToOrderProductSqlModel())
			.ToList();

		foreach(var add in orderProductAdd)
		{
			_orderProductRepository.Add(add);
		}

		_orderSqlRepository.Update(orderSql);
		await _orderSqlRepository.UnitOfWork.CommitAsync(cancellationToken);

	}
}
