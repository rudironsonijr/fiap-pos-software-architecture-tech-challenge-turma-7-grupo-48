using Infrastructure.SqlModels.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Reflection.Metadata;

namespace Infrastructure.Exceptions;

public class NotFoundException : Exception
{
	private const string DEFAULT_MESSAGE = "The {1} with {2}: {3} not found";
	private NotFoundException(string entityName, string parameter, string parameterValue) 
		: base(string.Format(DEFAULT_MESSAGE, entityName, parameter, parameterValue))
	{
		
	}

	public static void ThrowIfPropertyNull(object? value, Type entityType, string parameter, string parameterValue)
	{
		if (value == null)
		{
			throw new NotFoundException(entityType.ToString(), parameter, parameterValue);
		}
	}

	internal static void ThrowIfPropertyNull(object? value, Type entityType, string parameter, int parameterValue)
	{
		if (value == null)
		{
			throw new NotFoundException(entityType.ToString(), parameter, parameterValue.ToString());
		}
	}
}
