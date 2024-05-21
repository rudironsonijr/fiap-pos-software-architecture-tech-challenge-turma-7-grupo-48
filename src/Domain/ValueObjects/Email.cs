using System.Globalization;
using System.Text.RegularExpressions;
using Domain.ValueObjects.Exceptions;

namespace Domain.ValueObjects;

public struct Email
{
	public Email(string adress)
	{
		if (!IsValidEmail(adress))
			throw new InvalidEmailException(adress);
		Adress = adress;
	}

	public string Adress { get; }

	public static implicit operator Email(string adress)
	{
		return new Email(adress);
	}

	public static implicit operator string(Email email)
	{
		return email.Adress;
	}

	public static bool IsValidEmail(string email)
	{
		if (string.IsNullOrWhiteSpace(email))
			return false;

		try
		{
			// Normalize the domain
			email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, 
				RegexOptions.None, TimeSpan.FromMilliseconds(200));

			// Examines the domain part of the email and normalizes it.
			string DomainMapper(Match match)
			{
				// Use IdnMapping class to convert Unicode domain names.
				var idn = new IdnMapping();

				// Pull out and process domain name (throws ArgumentException on invalid)
				var domainName = idn.GetAscii(match.Groups[2].Value);

				return match.Groups[1].Value + domainName;
			}
		}
		catch (RegexMatchTimeoutException)
		{
			return false;
		}
		catch (ArgumentException)
		{
			return false;
		}

		try
		{
			return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
				RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
		}
		catch (RegexMatchTimeoutException)
		{
			return false;
		}
	}

	public override bool Equals(object? obj)
	{
		return obj is Email email &&
			   Adress == email.Adress;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Adress);
	}
}