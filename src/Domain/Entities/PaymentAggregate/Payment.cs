using Domain.Entities.Base.Interfaces;
using Domain.Entities.Enums;
using Domain.Entities.Exceptions;
using Domain.Entities.PaymentAggregate.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.PaymentAggregate;

public class Payment : IAggregateRoot
{
	public int Id { get; init; }
	private PaymentStatus _status;
	public required PaymentStatus Status 
	{
		get => _status;
		set 
		{
			UnableToChangePaymentStatusException.ThrowIfUnableToChangeStatus(_status);
			_status = value;
		} 
	}
	public required PaymentMethod PaymentMethod { get; init; }	
	public required int OrderId { get; init; }
	public required string ExternalId { get; init; }
	public required decimal Amount { get; init; }
}
