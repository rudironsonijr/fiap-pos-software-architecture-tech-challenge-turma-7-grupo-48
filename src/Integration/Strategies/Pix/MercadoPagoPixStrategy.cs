using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Gateways.Dtos.PaymentGateway;
using Domain.ValueObjects;
using Integration.Strategies.Pix.Interface;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;

namespace Integration.Strategies.Pix;

public class MercadoPagoPixStrategy : IPixPaymentStrategy
{
	public PaymentProvider paymentProvider => PaymentProvider.MercadoPago;
	static string AcessToken { get; } = Environment.GetEnvironmentVariable("MercadoPagoAcessToken") ?? throw new Exception("Enviroment Variable MercadoPagoAcessToken not Found");

	public MercadoPagoPixStrategy()
	{
		MercadoPagoConfig.AccessToken = AcessToken;
	}
	public async Task<CreatePixResponse> CreatePayment(Order order)
	{
		var paymentRequest = new PaymentCreateRequest
		{
			TransactionAmount = order.Price,
			Description = $"OrderId: {order.Id}",
			PaymentMethodId = "pix", // Para gerar QR Code PIX
			Installments = 1,
			StatementDescriptor = "Diners Tech challenge",
			Payer = new PaymentPayerRequest
			{
				Email = "meu@email2.com"
			}
		};

		var client = new PaymentClient();
		Payment payment = await client.CreateAsync(paymentRequest);

		if (payment.Status != MercadoPago.Resource.Payment.PaymentStatus.Pending)
		{
			throw new Exception("Falha ao criar o pagamento: " + payment.Status);
		}

		string qrCodeBase64 = payment.PointOfInteraction.TransactionData.QrCodeBase64;
		var qrCodeByte = Convert.FromBase64String(qrCodeBase64);
		var qrCodeImage = new Photo("qrCode.png", "image/png", qrCodeByte);

		
		return new CreatePixResponse()
		{
			ExternalId = payment.Id.ToString(),
			QrCode = qrCodeImage,
			Status = Domain.Entities.Enums.PaymentStatus.Pending,
		};
	}
}
