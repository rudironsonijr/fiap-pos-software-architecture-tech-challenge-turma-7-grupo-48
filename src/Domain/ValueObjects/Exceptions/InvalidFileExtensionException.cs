using Domain.Exceptions;

namespace Domain.ValueObjects.Exceptions;

public class InvalidFileExtensionException : DomainException
{
	private InvalidFileExtensionException(string extension, string allowedExtension)
		: base(string.Format(DefaultMessageTemplate, extension, allowedExtension))
	{

	}
	private const string DefaultMessageTemplate = "The Image extension {0} is invalid , allowed extensions {1}";

	public static void ThrowIfExtensionNotAllowed(string fileName, string[] allowedExtensions)
	{
		var ext = Path.GetExtension(fileName);

		if (allowedExtensions.Contains(ext))
		{
			return;
		}

		var allowedExtensionsJoin = string.Join(", ", allowedExtensions);

		throw new InvalidFileExtensionException(ext, allowedExtensionsJoin);
	}
}