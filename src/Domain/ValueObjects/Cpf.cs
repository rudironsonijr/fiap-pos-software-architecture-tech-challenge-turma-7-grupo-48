namespace Domain.ValueObjects;

public struct Cpf
{
	public Cpf(string number)
	{
		if (!IsValidCpf(number))
			throw new ArgumentException(
				"Invalid CPF",
				nameof(number)
			);

		Number = RemoveFormated(number);
	}

	public string Number { get; }

	public string FormatedNumber => Convert.ToUInt64(Number)
		.ToString(@"000\.000\.000\-00");

	public static implicit operator Cpf(string number)
	{
		return new Cpf(number);
	}

	public static implicit operator string(Cpf number)
	{
		return number.FormatedNumber;
	}

	public static string RemoveFormated(string number)
	{
		return number.Trim()
			.Replace(".", "")
			.Replace("-", "");
	}

	public static bool IsValidCpf(string number)
	{
		int[] multiplier1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
		int[] multiplier2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

		number = RemoveFormated(number);

		if (number.Length != 11)
		{
			return false;
		}

		for (var j = 0; j < 10; j++)
		{
			var stringValue = j.ToString();
			if (stringValue.PadLeft(11, char.Parse(stringValue)) == number)
			{
				return false;
			}
		}

		var hasCpf = number.Substring(0, 9);

		var sum = 0;

		for (var i = 0; i < 9; i++)
		{
			var valueString = hasCpf[i].ToString();
			var value = int.Parse(valueString);
			sum += value * multiplier1[i];
		}

		var remainder = sum % 11;

		if (remainder < 2)
		{
			remainder = 0;
		}
		else
		{
			remainder = 11 - remainder;
		}

		var digit = remainder.ToString();
		hasCpf = hasCpf + digit;
		sum = 0;

		for (var i = 0; i < 10; i++)
		{
			var valueString = hasCpf[i].ToString();
			var value = int.Parse(valueString);
			sum += value * multiplier2[i];

		}

		remainder = sum % 11;
		if (remainder < 2)
		{
			remainder = 0;
		}
		else
		{
			remainder = 11 - remainder;
		}

		digit = digit + remainder;

		return number.EndsWith(digit);
	}

	public override bool Equals(object? obj)
	{
		return obj is Cpf cpf &&
			   Number == cpf.Number &&
			   FormatedNumber == cpf.FormatedNumber;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Number, FormatedNumber);
	}
}