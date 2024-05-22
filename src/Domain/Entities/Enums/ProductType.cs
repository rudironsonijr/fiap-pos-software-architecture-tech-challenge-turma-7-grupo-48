using System.ComponentModel;

namespace Domain.Entities.Enums;

public enum ProductType
{
	None = 0,
	Drink = 1,
	[Description("Side Dish")] 
	SideDish = 2,
	Snack = 3,
	Dessert = 4
}