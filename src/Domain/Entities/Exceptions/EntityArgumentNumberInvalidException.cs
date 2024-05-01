namespace Domain.Entities.Exceptions
{
	public class EntityArgumentNumberInvalidException : Exception
	{
		public const string DefaultNumberInvalidGreaterOrEqualMessageTemplate =
			"The property {0} can't be greater than or equal {1} in model {2}";

		public const string DefaultNumberInvalidLessMessageTemplate =
			"The property {0} can't be less than or equal {1} in model {2}";

		private EntityArgumentNumberInvalidException(
			string message,
			string propertyName,
			decimal defaultValue,
			string entityName) : base(
				string.Format(
					format: message,
					arg0: propertyName,
					arg1: defaultValue,
					arg2 : entityName
					))
		{

		}

		public static void ThrowIfLessOrEqualThan(decimal minimalValue, decimal compareValue, string propertyName, string entityName)
		{
			if(compareValue <= minimalValue)
			{
				throw new EntityArgumentNumberInvalidException(DefaultNumberInvalidLessMessageTemplate, propertyName, minimalValue, entityName);
			}
		}



	}
}
