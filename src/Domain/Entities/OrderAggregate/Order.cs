using Domain.Entities.Base.Interfaces;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate.Exceptions;
using Domain.Entities.ProductAggregate;
using Domain.ValueObjects;

namespace Domain.Entities.OrderAggregate;

public class Order : IAggregateRoot
{
	public Order() { }

	public Order(int id, OrderStatus status, List<OrderProduct> orderProducts, PaymentMethod? paymentMethod, DateTime createdDate)
	{
		Id = id;
		Status = status;
		_orderProducts = orderProducts;
		_paymentMethod = paymentMethod;
		CreatedDate = createdDate;
	}

	public int Id { get; init; }

	public int? CustomerId { get; init; }

	public OrderStatus Status { get; private set; } = OrderStatus.Creating;

	private readonly List<OrderProduct> _orderProducts = [];
	public IEnumerable<OrderProduct> OrderProducts => _orderProducts;
	public DateTime? CreatedDate { get; init; }


	private PaymentMethod? _paymentMethod;
	public PaymentMethod? PaymentMethod
	{
		get => _paymentMethod;
		set
		{
			SetPaymentMethodInvalidException.ThrowInvalidStatus(Status);
			_paymentMethod = value;
		}
	}

	public decimal Price
	{
		get
		{
			var total = _orderProducts.Sum(x => x.Price);
			return Math.Round(total, 2);
		}
	}

	public Order AddProduct(Product product, int quantity)
	{
		ChangeOrderProductNotAbleException.ThrowInvalidStatus(Status);

		var item = _orderProducts.SingleOrDefault(x => x.ProductId == product.Id);

		if (item != null)
		{
			item.Quantity = quantity;
			return this;
		}

		OrderProduct orderProduct = new(product)
		{
			ProductPrice = product.Price,
			Quantity = quantity,
			OrderId = Id,
		};

		_orderProducts.Add(orderProduct);
		return this;
	}

	public Order RemoveProduct(int productId)
	{
		ChangeOrderProductNotAbleException.ThrowInvalidStatus(Status);

		var itemToRemove = _orderProducts.SingleOrDefault(x => x.ProductId == productId);

		if (itemToRemove != null)
		{
			_orderProducts.Remove(itemToRemove);
		}

		return this;
	}

	public Order UpdateProductQuantity(int orderProductId, int quantity)
	{
		ChangeOrderProductNotAbleException.ThrowInvalidStatus(Status);

		var itemToUpdate = _orderProducts.SingleOrDefault(x => x.Id == orderProductId);

		if (itemToUpdate == null)
		{
			return this;
		}

		if (quantity == 0)
		{
			_orderProducts.Remove(itemToUpdate);
			return this;
		}

		itemToUpdate.Quantity = quantity;
		return this;
	}

	public Order ChangeStatusToReceived()
	{
		ChangeOrderStatusInvalidException.ThrowIfOrderProductsIsEmpty(_orderProducts);
		ChangeOrderStatusInvalidException.ThrowIfOrderStatusInvalidStepChange(
			Status,
			OrderStatus.Creating,
			OrderStatus.Received);

		Status = OrderStatus.Received;
		return this;
	}

	public Order ChangeStatusToPreparing()
	{
		ChangeOrderStatusInvalidException.ThrowIfOrderStatusInvalidStepChange(
			Status,
			OrderStatus.Received,
			OrderStatus.Preparing);

		Status = OrderStatus.Preparing;
		return this;
	}

	public Order ChangeStatusToDone()
	{
		ChangeOrderStatusInvalidException.ThrowIfOrderStatusInvalidStepChange(
			Status,
			OrderStatus.Preparing,
			OrderStatus.Done);

		Status = OrderStatus.Done;
		return this;
	}

	public Order ChangeStatusToFinished()
	{
		ChangeOrderStatusInvalidException.ThrowIfOrderStatusInvalidStepChange(
			Status,
			OrderStatus.Done,
			OrderStatus.Finished);

		Status = OrderStatus.Finished;
		return this;
	}

	public Order ChangeStatusToCancelled()
	{
		ChangeOrderStatusInvalidException.ThrowIfOrderStatusInvalidStepChange(
			Status,
			OrderStatus.Creating,
			OrderStatus.Cancelled);

		Status = OrderStatus.Cancelled;
		return this;
	}
}