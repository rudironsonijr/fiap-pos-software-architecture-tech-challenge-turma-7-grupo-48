using System.ComponentModel;

namespace Helpers;

public static class EnumHelpers
{
	public static string GetEnumDescription<T>(this T value) where T : Enum
	{
		var fi = value.GetType()
			.GetField(value.ToString());

		var attributes = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

		if (attributes != null && attributes.Any())
		{
			return attributes.First()
				.Description;
		}
			
		return value.ToString();
	}
}