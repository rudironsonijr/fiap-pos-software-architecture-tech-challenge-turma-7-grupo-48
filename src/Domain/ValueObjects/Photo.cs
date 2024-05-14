using Domain.ValueObjects.Exceptions;

namespace Domain.ValueObjects;

public struct Photo
{
	public Photo(string fileName, string contetType, byte[] data)
	{
		var allowedExtensions = new string[] 
		{ 
			".jpg", 
			".png", 
			".jpeg",
			".svg"
		};

		InvalidFileExtensionException.ThrowIfExtensionNotAllowed(fileName, allowedExtensions);

		FileName = fileName;
		ContentType = contetType;
		Data = data;

	}

	public string FileName { get; }
	public string ContentType { get; }
	public byte[] Data { get;}
}
