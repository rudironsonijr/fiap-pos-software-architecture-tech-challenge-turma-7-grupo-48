using System.ComponentModel;

namespace Domain.Entities.Enums
{
	public enum OrderStatus
	{
		None = 0,
		Received = 1,
		Preparing = 2,
		[Description("Order Soon")]
		OrderSoon = 3,
		Finished = 4

	}
}
