using System.ComponentModel;

namespace Domain.Entities.Enums;

public enum OrderStatus
{
	None = 0,
	Creating = 1,
	Received = 3,
	Preparing = 4,
	Done = 5,
	Finished = 6,
	Cancelled = 7
}