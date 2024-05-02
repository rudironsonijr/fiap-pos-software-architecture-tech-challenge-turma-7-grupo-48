namespace Domain.Entities.OrderAggregate.Exceptions
{
	public class ChangeOrderProductNotAbleException() : 
		Exception(DefaultChangeOrderProductNotAbleMessageTemplate)
	{
		private const string DefaultChangeOrderProductNotAbleMessageTemplate = "Is not possiblea changer product when order is not Creating";
	}
}
