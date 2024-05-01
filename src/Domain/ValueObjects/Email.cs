using System.Globalization;
using System.Text.RegularExpressions;
using Domain.ValueObjects.Exceptions;

namespace Domain.ValueObjects;

public struct Email
{
	public Email(string adress)
	{
		if (!IsValidEmail(email: adress))
			throw new InvalidEmailException(address: adress);
		Adress = adress;
	}

	public string Adress { get; }

	public static implicit operator Email(string adress)
	{
		return new Email(adress: adress);
	}

	public static implicit operator string(Email email)
	{
		return email.Adress;
	}

	public static bool IsValidEmail(string email)
	{
		if (string.IsNullOrWhiteSpace(value: email))
			return false;

		try
		{
			// Normalize the domain
			email = Regex.Replace(
				input: email,
				pattern: @"(@)(.+)$",
				evaluator: DomainMapper,
				options: RegexOptions.None,
				matchTimeout: TimeSpan.FromMilliseconds(value: 200)
			);

			// Examines the domain part of the email and normalizes it.
			string DomainMapper(Match match)
			{
				// Use IdnMapping class to convert Unicode domain names.
				var idn = new IdnMapping();

				// Pull out and process domain name (throws ArgumentException on invalid)
				var domainName = idn.GetAscii(unicode: match.Groups[groupnum: 2].Value);

				return match.Groups[groupnum: 1].Value + domainName;
			}
		}
		catch (RegexMatchTimeoutException e)
		{
			return false;
		}
		catch (ArgumentException e)
		{
			return false;
		}

		try
		{
			return Regex.IsMatch(
				input: email,
				pattern: @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
				options: RegexOptions.IgnoreCase,
				matchTimeout: TimeSpan.FromMilliseconds(value: 250)
			);
		}
		catch (RegexMatchTimeoutException)
		{
			return false;
		}
	}
}