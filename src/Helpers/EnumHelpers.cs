using System.ComponentModel;
using System.Reflection;

namespace Helpers;

public static class EnumHelpers
{
	public static string GetEnumDescription<T>(this T value) where T : Enum
	{
		FieldInfo? fi = value.GetType().GetField(value.ToString());

		DescriptionAttribute[]? attributes = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

		if (attributes != null && attributes.Any())
		{
			return attributes.First().Description;
		}

		return value.ToString();
	}
}
