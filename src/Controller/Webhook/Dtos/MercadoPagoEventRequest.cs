using System.Text.Json.Serialization;

namespace Adapters.Webhook.Dtos;

public record MercadoPagoEventRequest
{
		public string Action { get; init; } = string.Empty;

		public MercadoPagoDataRequest Data { get; init; } = new MercadoPagoDataRequest();

		[JsonPropertyName("date_created")]
		public DateTime DateCreated { get; init; }

		public long Id { get; init; }

		[JsonPropertyName("user_id")]
		public long UserId { get; init; }

}
