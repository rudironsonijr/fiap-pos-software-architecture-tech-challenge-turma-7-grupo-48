using Domain.Entities.Enums;
using Domain.ValueObjects;

namespace Domain.Gateways.Dtos.PaymentGateway;

public record CreatePixResponse
{
	public required Photo QrCode { get; init; }
	public required string ExternalId { get; init; } 
	public required PaymentStatus Status { get; init; }
}
