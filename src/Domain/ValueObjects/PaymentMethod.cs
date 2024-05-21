using Domain.Entities.Enums;
using Domain.ValueObjects.Exceptions;
using System.Collections.ObjectModel;

namespace Domain.ValueObjects;

public struct PaymentMethod
{
	public PaymentProvider Provider { get; }
	public PaymentMethodKind Kind { get; }

	public PaymentMethod(PaymentProvider provider, PaymentMethodKind kind)
	{
		Provider = provider;
		Kind = kind;

		Validate();
	}

	private void Validate()
	{
		bool isSuported = Provider switch
		{
			PaymentProvider.MercadoPago => MercadoPagoSupportedKind(Kind),
			_ => throw new NotImplementedException()
		};

		if (isSuported)
		{
			return;
		}

		throw new PaymentMethodKindNotSupportedException(Kind, Provider);
	}

	public static bool MercadoPagoSupportedKind(PaymentMethodKind paymentMethodKind)
	{
		var supported = new Collection<PaymentMethodKind>() 
		{ 
			PaymentMethodKind.Pix 
		};

		return supported.Contains(paymentMethodKind);
	}

	public override bool Equals(object? obj)
	{
		return obj is PaymentMethod method &&
			   Provider == method.Provider &&
			   Kind == method.Kind;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Provider, Kind);
	}
}
