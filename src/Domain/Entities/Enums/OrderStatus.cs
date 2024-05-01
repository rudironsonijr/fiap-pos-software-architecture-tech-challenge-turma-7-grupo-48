using System.ComponentModel;

namespace Domain.Entities.Enums
{
	public enum OrderStatus
	{
		None = 0,
		Creating = 1,
		Received = 2,
		Preparing = 3,
		[Description("Order Soon")]
		OrderSoon = 4,
		Finished = 5

	}
}
